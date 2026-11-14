using UnityEngine;

public class MultiplierManager : MonoBehaviour
{
    //linked to UIManager
    /*new rides create a happiness level to maintain and when they leave behind mess it lowers the total level a bit,
    cleaning the map raises happiness level of park giving a higher money multiplier?
    the multiplier meter starts at 0 when the day starts and raises when people go on rides,*/
    public float meterMax { get; private set; }
    public float meterCurrent { get; private set; }

    private float waitUntil;
    private float waitingTime = 1;

    void Update()
    {
        meterCurrent = Mathf.Round(meterCurrent * 1000f) / 1000f;
        StartWaiting();
        FinishWaiting();
    }

    private void StartWaiting()
    {
        if (waitUntil + 1 <= Time.fixedTime) //magic number to control order of operations
        {
            waitUntil = Time.fixedTime + waitingTime;
            //Debug.Log($"{currentTask.name},{waitUntil}, {currentTask.WaitTime}, {Time.fixedTime}");
        }
    }

    private void FinishWaiting()
    {
        if (waitUntil < Time.fixedTime)
        {

            GameManager.manager.player.IncreaseMoney(1f * meterCurrent);
        }
    }

    public void IncreaseMeterMax(float increase)
    {
        meterMax += increase;

        MarkiplierChange();
    }

    public void DecreaseMeterMax(float decrease) //probably unused
    {
        meterMax -= decrease;

        MarkiplierChange();
    }

    public void IncreaseMeterCurrent(float increase)
    { 
        if (meterCurrent < meterMax)
            meterCurrent += increase;

        if (meterCurrent > meterMax)
            meterCurrent = meterMax;

        MarkiplierChange();
    }

    public void DecreaseMeterCurrent(float decrease)
    {
        if (meterCurrent == 0) return;

        if (meterCurrent > 0)
            meterCurrent -= decrease;

        if (meterCurrent < 0.000f)
            meterCurrent = 0;

        MarkiplierChange();
    }

    private void MarkiplierChange()
    {
        //Debug.Log($"multiplier changed {meterCurrent}/{meterMax}");
    }
}
