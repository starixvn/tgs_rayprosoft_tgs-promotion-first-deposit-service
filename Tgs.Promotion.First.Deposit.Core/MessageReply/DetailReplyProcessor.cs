using System.Text.Json;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using TGS.Core.Infrastructures.Nats.Interface;
using Tgs.Promotion.Clients;

namespace Tgs.Promotion.First.Deposit.Core.MessageReply;

public class DetailReplyProcessor(ILogger<DetailReplyProcessor> logger, IPromotionServiceClient promotionServiceClient)
    : IMessageReplyHandler<DistributedDetailRequest, DistributedDetailResponse>
{
    public async Task<DistributedDetailResponse> Handle(string subject, string? queueGroup,
        NatsHeaders? natsHeaders, DistributedDetailRequest? message,
        CancellationToken cancellationToken)
    {
        var distribution =
            await promotionServiceClient.GetPromotionDistributeByReferenceIdAsync(message.ReferenceId,
                cancellationToken);

        var response = new DistributedDetailResponse
        {
            Detail = distribution != null
                ? JsonSerializer.Serialize(
                    new
                    {
                        distribution.BonusAmount
                    },
                    PromotionUtility.SerializerOptions)
                : null
        };

        return response;
    }
}