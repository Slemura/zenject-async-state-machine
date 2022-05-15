namespace modules.state_machine.sample {
    public class CallCounter {
        
        private int _count = 0;
        public  int Count => _count;

        public void Increment() {
            _count++;
        }
    }
}