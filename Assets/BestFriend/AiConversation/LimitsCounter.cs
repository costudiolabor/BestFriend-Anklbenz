using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LimitsCounter {
	public event Action RefreshSuccessEvent;
	public bool hasLimit => _requestsAvailable > 0;
	public bool hasMax => _requestsAvailable == _maxRequests;
	public DateTime timeToNextIteration { get => _timeToNextIteration; private set => _timeToNextIteration = value; }
	public int requestsAvailable { get => _requestsAvailable; private set => _requestsAvailable = value; }
	public int maxRequests { get => _maxRequests; private set => _maxRequests = value; }

	private DateTime _timeToNextIteration;
	private long _incrementTimeMilliseconds, _timeDelta, _lastUpdateTime;
	private int _requestsAvailable, _maxRequests;

	public async UniTask Initialize(ConversationsSettings settings) {
		var serverTimeRequest = await DatabaseApi.instance.GetServerTime();
		_timeDelta = GetLocalFromGlobalTimeDelta(serverTimeRequest.data.now);
		_incrementTimeMilliseconds = settings.freeMessageIncrementTimeMilliseconds;
		_maxRequests = settings.freeMessagesCount;

	}
	public async UniTask Refresh() {
		await GetLimits();
		var requestsEarned = GetEarnedRequestsCount();
		if (requestsEarned < 1) return;

		var limits = CalculateLimits(requestsEarned);

		await SaveLimitValues(limits);
		RefreshSuccessEvent?.Invoke();
	}
	private Limits CalculateLimits(int requestsEarned) {
		_requestsAvailable = Mathf.Clamp(_requestsAvailable + requestsEarned, 0, _maxRequests);
		_lastUpdateTime += requestsEarned * _incrementTimeMilliseconds;
		_timeToNextIteration = GetLocalTimeToNextIteration();

		return new Limits() {lastRequest = _lastUpdateTime, requestsAvailable = _requestsAvailable};
	}
	private async UniTask<bool> GetLimits() {
		var limitsRequest = await DatabaseApi.instance.GetLimits();
		if (!limitsRequest.isSuccess) return false;

		_requestsAvailable = limitsRequest.data.requestsAvailable;
		_lastUpdateTime = limitsRequest.data.lastRequest;

		return true;
	}
	private DateTime GetLocalTimeToNextIteration() {
		var newDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return newDateTime.AddMilliseconds(ServerToLocalTimeUnix(_lastUpdateTime + _incrementTimeMilliseconds));
	}
	private int GetEarnedRequestsCount() {
		var currentServerTime = LocalToServerTimeUnix(DateTime.UtcNow);
		var timePassedMilliseconds = currentServerTime - _lastUpdateTime;
		return (int)(timePassedMilliseconds / _incrementTimeMilliseconds);
	}
	private long GetLocalFromGlobalTimeDelta(long time) =>
			((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - time;
	
	
	private long ServerToLocalTimeUnix(long timeUnix) =>
			timeUnix + _timeDelta;
	
	private long LocalToServerTimeUnix(DateTime time) =>
			((DateTimeOffset)time).ToUnixTimeMilliseconds() - _timeDelta;
	
	private async UniTask<bool> SaveLimitValues(Limits limits) =>
			(await DatabaseApi.instance.SetLimits(limits)).isSuccess;
}