namespace CatFactsCore.Domain.Constants
{
    public class SmsReplies
    {
        public const string Welcome =
            "😸 Meow! Welcome to cat facts. You can unsubscribe at any time by replying stop.";

        public const string Goodbye =
            "😿 Farewell from cat facts! You can re-subscribe at any time by replying start.";

        public const string Help =
            "🐾 This is cat facts. Reply start to start receiving a cat fact by text once daily. Reply stop to cancel your subscription.";

        public const string Cuss = "😾 $@#&!";

        public static string Error = $"{Cuss} Something went wrong! Server Cat is looking into it!";
    }
}