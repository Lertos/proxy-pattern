namespace proxy_pattern
{
    //A demonstration of the Proxy pattern in C#
    public class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            //Using the real subject directly
            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new RealSubject();

            client.ClientCode(realSubject);

            Console.WriteLine();

            //Using the proxy instead of the client directly
            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);

            client.ClientCode(proxy);

            /* OUTPUT:
             * 
             * Client: Executing the client code with a real subject:
             * RealSubject: Handling Request.

             * Client: Executing the same client code with a proxy:
             * Proxy: Checking access prior to firing a real request.
             * RealSubject: Handling Request.
             * Proxy: Logging the time of request.
             *
             */
        }
    }

    //The Subject interface declares common operations for both RealSubject and the Proxy.
    //As long as the client works with RealSubject using this interface, you'll be able to pass it a proxy instead of a real subject.
    public interface ISubject
    {
        void Request();
    }

    //RealSubject contains core business logic - some useful work which may also be very slow or sensitive.
    //A Proxy can solve these issues without any changes to the RealSubject's code.
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }

    //The Proxy uses the interface that RealSubject uses
    public class Proxy : ISubject
    {
        //Always keep an instance of the RealSubject
        private RealSubject realSubject;

        //Pass in the RealSubject so we can always have access to it
        public Proxy(RealSubject realSubject)
        {
            this.realSubject = realSubject;
        }

        //A Proxy performs whatever logic is desired, and then passes the execution to the same method in the linked RealSubject object
        public void Request()
        {
            //Would be used to check if the client should be allowed to request this
            if (this.CheckAccess())
            {
                //Perform the real request
                this.realSubject.Request();

                //Perform the extra logging that we desire
                this.LogAccess();
            }
        }

        public bool CheckAccess()
        {
            //Real checks would go here
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }

    public class Client
    {
        //The client code is supposed to work with all objects (both subjects and proxies) via the ISubject interface.
        //In reality, clients mostly work with their real subjects directly. Instead, you can extend your proxy from the real subject's class
        public void ClientCode(ISubject subject)
        {
            subject.Request();
        }
    }
}
