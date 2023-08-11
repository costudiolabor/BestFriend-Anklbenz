using System;
using Cysharp.Threading.Tasks;

public class LimitsCounter {
	public event Action RefreshEvent; 
	public bool isEmpty => _requestsAvailable == 0;
	public bool isMax => _requestsAvailable == _maxRequests;

	public bool isReady { get; private set; }
	public int requestsAvailable => _requestsAvailable;

	public long timeLeftToNextMilliseconds => _nextIterationTimeInLocal - ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

	private DateTime _timeToNextIteration;
	private long _incrementTimeMilliseconds, _timeDelta, _lastUpdateTime, _nextIterationTimeInLocal;
	private int _requestsAvailable, _maxRequests;

	public void Initialize(FreeLimits limits) {
		_incrementTimeMilliseconds = limits.messageIncrementTimeMilliseconds;
		_maxRequests = limits.messagesCount;
	}

	public async UniTask Refresh() {
		isReady = false;
		var limitsRequest = await DatabaseApi.instance.GetActualLimits();

		if (!limitsRequest.isSuccess) return;
		_lastUpdateTime = limitsRequest.data.lastRequest;
		_requestsAvailable = limitsRequest.data.requestsAvailable;

		_timeDelta = GetLocalFromGlobalTimeDelta(limitsRequest.data.nowUnix);
		_nextIterationTimeInLocal = (_lastUpdateTime + _timeDelta) + _incrementTimeMilliseconds;

		isReady = true;
		RefreshEvent?.Invoke();
	}

	private long GetLocalFromGlobalTimeDelta(long time) =>
			((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() - time;
}
/*private async UniTask<bool> SaveLimitValues(Limits limits) =>
		(await DatabaseApi.instance.SetLimits(limits)).isSuccess;*/

/*public async UniTask Initialize(ConversationsSettings settings) {
//	var serverTimeRequest = await DatabaseApi.instance.GetServerTime();
	_timeDelta = GetLocalFromGlobalTimeDelta(serverTimeRequest.data.now);
	_incrementTimeMilliseconds = settings.freeMessageIncrementTimeMilliseconds;
	_maxRequests = settings.freeMessagesCount;
}*/
/*public async UniTask Refresh() {
	await GetLimits();
//		var requestsEarned = GetEarnedRequestsCount();
	//	if (requestsEarned < 1) return;

	//	var limits = CalculateLimits(requestsEarned);

	//await SaveLimitValues(limits);
	RefreshSuccessEvent?.Invoke();
}*/
/*private Limits CalculateLimits(int requestsEarned) {
	_requestsAvailable = Mathf.Clamp(_requestsAvailable + requestsEarned, 0, _maxRequests);
	_lastUpdateTime += requestsEarned * _incrementTimeMilliseconds;
	_timeToNextIteration = GetLocalTimeToNextIteration();

	return new Limits() {lastRequest = _lastUpdateTime, requestsAvailable = _requestsAvailable};
}*/

/*private int GetEarnedRequestsCount() {
	var currentServerTime = LocalToServerTimeUnix(DateTime.UtcNow);
	var timePassedMilliseconds = currentServerTime - _lastUpdateTime;
	return (int)(timePassedMilliseconds / _incrementTimeMilliseconds);
}*/

/*private long LocalToServerTimeUnix(DateTime time) =>
		((DateTimeOffset)time).ToUnixTimeMilliseconds() - _timeDelta;
		
	private DateTime GetLocalTimeToNextIteration() {
		var newDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return newDateTime.AddMilliseconds(ServerToLocalTimeUnix(_lastUpdateTime + _incrementTimeMilliseconds));
	}
	private long ServerToLocalTimeUnix(long timeUnix) =>
			timeUnix + _timeDelta;
*/