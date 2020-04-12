public class LevelData
{
    public byte[,] Layout { get; private set; }
    public byte[,] Waves { get; private set; }
    public LevelData(byte[,] p_layout, byte[,] p_waves)
    {
        Layout = p_layout;
        Waves = p_waves;
    }
}
