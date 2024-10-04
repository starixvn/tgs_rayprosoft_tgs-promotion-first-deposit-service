using TGS.Core.Infrastructures.Nats.Interface;
using Tgs.Payment.Clients;
using Tgs.Payment.Protos;
using Tgs.Promotion.First.Deposit.Core.ApplicationServices;
using Tgs.ServiceUtility;
using NATS.Client.Core;

namespace Tgs.Promotion.First.Deposit.Core.MessageReply;

internal class DepositFormStatusChangedHandler(
    IPaymentServiceClient paymentServiceClient,
    ICommandService<DistributeFirstDepositBonusCommand> distributeService)
    : IMessageHandler<FormEventEx>
{
    public async Task Handle(string subject, string? queueGroup, NatsHeaders? natsHeaders, FormEventEx? message,
        CancellationToken cancellationToken = default)
    {
        var previousStatus = Enum.Parse<PaymentFlowState>(message.Status);

        var depositForm = await paymentServiceClient.GetDepositFormAsync(
            message.FormId,
            cancellationToken).ConfigureAwait(false);

        if (depositForm?.PromotionId is null)
            return;

        if (previousStatus != depositForm.Status
            && depositForm.Status == PaymentFlowState.Approved)
        {
            var command = new DistributeFirstDepositBonusCommand(depositForm);

            await distributeService.ExecuteAsync(
                command,
                cancellationToken).ConfigureAwait(false);
        }
    }
}
