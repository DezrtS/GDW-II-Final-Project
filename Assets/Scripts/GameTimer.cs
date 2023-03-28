using UnityEngine;

public class GameTimer
{
    private float timeTillCompletion = 1f;
    private float currentTimeDifference = 0f;
    private float startingTime = 0f;

    private int timerCompletionCount = 0;

    private bool timerAlreadyFinished = false;
    private bool paused;

    public GameTimer(float timeTillCompletion, bool paused)
    {
        this.timeTillCompletion = timeTillCompletion;
        this.paused = paused;
    }

    public bool UpdateTimer()
    {
        if (paused)
        {
            return false;
        }

        if (!timerAlreadyFinished && currentTimeDifference >= timeTillCompletion)
        {
            timerCompletionCount++;
            timerAlreadyFinished = true;
        }
        currentTimeDifference = Time.timeSinceLevelLoad - startingTime;
        return (currentTimeDifference >= timeTillCompletion);
    }

    public void SetTimeTillCompletion(float timeTillCompletion)
    {
        this.timeTillCompletion = timeTillCompletion;
    }

    public void RestartTimer()
    {
        //Debug.Log("The timer was " + (currentTimeDifference - timeTillCompletion) + " seconds off");
        currentTimeDifference = 0f;
        startingTime = Time.timeSinceLevelLoad;
        timerAlreadyFinished = false;
    }

    public void PauseTimer(bool paused)
    {
        this.paused = paused;
        startingTime = Time.timeSinceLevelLoad - currentTimeDifference;
    }

    public int GetTimerCompletionCount()
    {
        return timerCompletionCount;
    }

    public bool GetTimerAlreadyFinished()
    {
        return timerAlreadyFinished;
    }
}