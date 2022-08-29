using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/Game", order = 1)]
    public class GameData : ScriptableObject
    {
        #region Player Data
        #region Stats
        public float Speed = 3;
        public int Weapon;
        #endregion

        #region Skin
        public int Color;
        public int Pant;
        public int Hair;
        public int Set = 0;
        #endregion

        #region Poverty
        public int HighestRank = 100;
        public int CurrentRegion = 1;
        #endregion
        #endregion

        public void SetDataState(string data, int ID, int state)
        {
            PlayerPrefs.SetInt(data + ID, state);
        }

        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = selected
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ID"></param>
        /// <param name="state"></param>
        public int GetDataState(string data, int ID, int defaultID = 0)
        {
            return PlayerPrefs.GetInt(data + ID, defaultID);
        }

        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = selected
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ID"></param>
        /// <param name="state"></param>
        public void SetDataState(string data, int ID, ref List<int> variable, int state)
        {
            variable[ID] = state;
            PlayerPrefs.SetInt(data + ID, state);
        }

        /// <summary>
        /// Key_Name
        /// if(bool) true == 1
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        public void SetIntData(string data, ref int variable, int value)
        {
            variable = value;
            PlayerPrefs.SetInt(data, value);
        }

        public void SetBoolData(string data, ref bool variable, bool value)
        {
            variable = value;
            PlayerPrefs.SetInt(data, value ? 1 : 0);
        }

        public void SetFloatData(string data, ref float variable, float value)
        {
            variable = value;
            PlayerPrefs.GetFloat(data, value);
        }

        public void SetStringData(string data, ref string variable, string value)
        {
            variable = value;
            PlayerPrefs.SetString(data, value);
        }
        public void OnInitData()
        {
            #region Player Data
            #region Stats
            Speed = PlayerPrefs.GetFloat(Player.P_SPEED,3);
            Weapon = PlayerPrefs.GetInt(Player.P_WEAPON, (int)PoolID.Weapon_Arrow);
            #endregion

            #region Skins
            Color = PlayerPrefs.GetInt(Player.P_COLOR,0);
            Pant = PlayerPrefs.GetInt(Player.P_PANT,(int)PantSkin.Batman);
            Hair = PlayerPrefs.GetInt(Player.P_HAIR,(int)PoolID.Hair_Arrow);
            Set = PlayerPrefs.GetInt(Player.P_SET,0);
            #endregion
            #region Poverty
            HighestRank = PlayerPrefs.GetInt(Player.P_HIGHTEST_SCORE,100);
            CurrentRegion = PlayerPrefs.GetInt(Player.P_CURRENT_REGION,1);
            #endregion
            #endregion
        }
    }
}