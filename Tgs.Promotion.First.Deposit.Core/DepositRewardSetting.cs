namespace Tgs.Promotion.First.Deposit.Core;
public record DepositRewardSetting(
	decimal Percentage,
	decimal BonusAmountLimit,
	decimal TurnoverMultiple,
	int[] VipLevels,
	string[]? BucketTypes);
