using System.Text.Json;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using Tgs.Promotion.Clients.Protos;
using TGS.Core.Infrastructures.Nats.Interface;

namespace Tgs.Promotion.First.Deposit.Core.MessageReply;

public class DepositRuleReplyProcessor(ILogger<DepositRuleReplyProcessor> logger)
    : IMessageReplyHandler<CampaignDetailRequest, CampaignDetailResponse>
{
    public Task<CampaignDetailResponse> Handle(
        string subject, string? queueGroup, NatsHeaders? natsHeaders,
        CampaignDetailRequest? message,
        CancellationToken cancellationToken = default)
    {
        var settings = JsonSerializer.Deserialize<DepositRewardSetting>(
                           message.Settings,
                           PromotionUtility.SerializerOptions) ??
                       throw new NullReferenceException("Deserialize DepositRewardSetting is null");

        logger.LogDebug("First deposit setting : {settings}", settings);

        settings = settings with
        {
            BucketTypes = settings.BucketTypes?.Where(v => !string.IsNullOrWhiteSpace(v)).ToArray()
        };

        var response = new CampaignDetailResponse
        {
            CampaignDetail = JsonSerializer.Serialize(
                settings,
                PromotionUtility.SerializerOptions),
            BonusAmountLimit = settings.BonusAmountLimit,
            Percentage = settings.Percentage,
            TurnoverMultiple = settings.TurnoverMultiple
        };

        return Task.FromResult(response);
    }
}