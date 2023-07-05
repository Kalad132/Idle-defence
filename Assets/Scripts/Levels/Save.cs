using UnityEngine;

namespace Levels
{
    public class Save : MonoBehaviour
    {
        public bool Registred => PlayerPrefs.HasKey(Parametrs.RegDate);

        public int GetLevel()
        {
            if (PlayerPrefs.HasKey(Parametrs.Level) == false)
                PlayerPrefs.SetInt(Parametrs.Level, 1);
            return PlayerPrefs.GetInt(Parametrs.Level);
        }

        public void SetLevel(int level)
        {
            PlayerPrefs.SetInt(Parametrs.Level, level);
            PlayerPrefs.Save();
        }

        public void IncreaseLevel()
        {
            PlayerPrefs.SetInt(Parametrs.Level, GetLevel() + 1);
            PlayerPrefs.Save();
        }

        public void ResetLevel()
        {
            PlayerPrefs.SetInt(Parametrs.Level, 1);
            PlayerPrefs.Save();
        }

        public void SetRegistrationDate(string date)
        {
            PlayerPrefs.SetString(Parametrs.RegDate, date);
        }

        public string GetRegistrationDate()
        {
            return PlayerPrefs.GetString(Parametrs.RegDate);
        }

        public void SetUpgradeDamage(int value)
        {
            PlayerPrefs.SetInt(Parametrs.UpgradeDamage, value);
        }

        public int GetUpgradeDamage()
        {
            if (PlayerPrefs.HasKey(Parametrs.UpgradeDamage) == false)
                PlayerPrefs.SetInt(Parametrs.UpgradeDamage, 0);
            return PlayerPrefs.GetInt(Parametrs.UpgradeDamage);
        }

        public void SetUpgradeHP(int value)
        {
            PlayerPrefs.SetInt(Parametrs.UpgradeHP, value);
        }

        public int GetUpgradeHP()
        {
            if (PlayerPrefs.HasKey(Parametrs.UpgradeHP) == false)
                PlayerPrefs.SetInt(Parametrs.UpgradeHP, 0);
            return PlayerPrefs.GetInt(Parametrs.UpgradeHP);
        }

        public void SetUpgradeHands(int value)
        {
            PlayerPrefs.SetInt(Parametrs.UpgradeHands, value);
        }

        public int GetUpgradeHands()
        {
            if (PlayerPrefs.HasKey(Parametrs.UpgradeHands) == false)
                PlayerPrefs.SetInt(Parametrs.UpgradeHands, 0);
            return PlayerPrefs.GetInt(Parametrs.UpgradeHands);
        }



        public class Parametrs
        {
            public static string Level => nameof(Level);
            public static string RegDate => nameof(RegDate);
            public static string UpgradeDamage => nameof(UpgradeDamage);
            public static string UpgradeHP => nameof(UpgradeHP);
            public static string UpgradeHands => nameof(UpgradeHands);

        }
    }
}