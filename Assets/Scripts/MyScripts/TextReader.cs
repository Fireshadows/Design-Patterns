using UnityEngine;

public static class TextReader
{
    public static LevelData ProcessText(TextAsset p_textAsset)
    {
        string[] m_lines = p_textAsset.text.Split(new char[] {'#'}, System.StringSplitOptions.RemoveEmptyEntries);

        string m_waveDataText = m_lines[1];

        char[] charsToTrim = { '\t', '\r' };

        string[] m_layoutText = m_lines[0].Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        m_waveDataText = m_waveDataText.Trim(charsToTrim);

        //Process Layout data
        byte[,] m_layoutData = new byte[m_layoutText.Length, m_layoutText[0].Length-1];
        
        for (int i = 0; i < m_layoutData.GetLength(0); i++)
        {
            for (int j = 0; j < m_layoutData.GetLength(1); j++)
            {
                double m_char = char.GetNumericValue(m_layoutText[i][j]);
                m_layoutData[i, j] = m_char > -1? (byte)m_char : byte.MinValue;
            }
        }

        //Process Enemy data
        string[] m_enemyRows = m_waveDataText.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        byte[,] m_enemyWaveData = new byte[m_enemyRows.Length, 2];

        for (int i = 0; i < m_enemyWaveData.GetLength(0); i++)
        {
            string[] m_enemyRow = m_enemyRows[i].Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < m_enemyWaveData.GetLength(1); j++)
                m_enemyWaveData[i, j] = (byte)int.Parse(m_enemyRow[j]);
        }

        return new LevelData(m_layoutData, m_enemyWaveData);
    }
}
