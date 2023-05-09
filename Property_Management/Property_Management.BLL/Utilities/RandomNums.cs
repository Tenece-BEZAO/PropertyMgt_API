namespace Property_Management.BLL.Utilities
{
    public class RandomNums
    {
        public static int GenerateRandomNumbers()
        {
            Random random = new((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999);
        }
    }
}
