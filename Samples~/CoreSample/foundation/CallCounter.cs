namespace RpDev.AsyncStateMachine.Sample
{
    public class CallCounter
    {
        public int Count { get; private set; } = 0;

        public void Increment()
        {
            Count++;
        }
    }
}