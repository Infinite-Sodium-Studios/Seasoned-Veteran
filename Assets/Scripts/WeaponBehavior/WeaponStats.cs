public class WeaponStats
{
    private int[] compatibleWith;

    public WeaponStats()
    {
        compatibleWith = new int[0];
    }
    public WeaponStats(int[] _compatibleWith)
    {
        compatibleWith = _compatibleWith;
    }

    public bool IsCompatibleWithEnemy(int enemyIndex)
    {
        foreach (var compatibleIndex in compatibleWith)
        {
            if (compatibleIndex == enemyIndex)
            {
                return true;
            }
        }
        return false;
    }
}
