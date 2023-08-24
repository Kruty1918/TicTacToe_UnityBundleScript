using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Studio29.TicTacToe
{
    public class GameBoard : MonoBehaviour
    {
        #region Variable

        [SerializeField] private Button[] cells;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Button restartButton;

        private char currentPlayer = 'X';
        private bool isGameOver = false;
        private Dictionary<int, TextMeshProUGUI> cellTexts = new Dictionary<int, TextMeshProUGUI>();

        #endregion

        #region Unity Callback

        private void Start()
        {
            InitializeGame();
        }

        #endregion

        #region Logic

        private void InitializeGame()
        {
            messageText.text = "Починає гравець X";

            restartButton.onClick.AddListener(RestartGame);
            restartButton.gameObject.SetActive(false);

            for (int i = 0; i < cells.Length; i++)
            {
                int index = i;
                cells[i].onClick.AddListener(() => CellClick(index));
                TextMeshProUGUI cellText = cells[i].GetComponentInChildren<TextMeshProUGUI>();
                cellText.text = "";
                cellTexts.Add(index, cellText);
                cells[i].interactable = true;
            }

            isGameOver = false;
        }

        private void CellClick(int cellIndex)
        {
            if (!isGameOver && cells[cellIndex].interactable)
            {
                cells[cellIndex].interactable = false;
                cellTexts[cellIndex].text = currentPlayer.ToString();

                if (CheckWin())
                {
                    messageText.text = "Гравець " + currentPlayer + " переміг!";
                    restartButton.gameObject.SetActive(true);
                    isGameOver = true;
                }
                else if (CheckDraw())
                {
                    messageText.text = "Нічия!";
                    restartButton.gameObject.SetActive(true);
                    isGameOver = true;
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    messageText.text = "Хід гравця " + currentPlayer;
                }
            }
        }

        private bool CheckWin()
        {
            // Перевірка по горизонталі, вертикалі та діагоналі
            for (int i = 0; i < 3; i++)
            {
                if (cellTexts[i].text == currentPlayer.ToString() &&
                    cellTexts[i + 3].text == currentPlayer.ToString() &&
                    cellTexts[i + 6].text == currentPlayer.ToString())
                {
                    return true;
                }

                if (cellTexts[i * 3].text == currentPlayer.ToString() &&
                    cellTexts[i * 3 + 1].text == currentPlayer.ToString() &&
                    cellTexts[i * 3 + 2].text == currentPlayer.ToString())
                {
                    return true;
                }
            }

            if (cellTexts[0].text == currentPlayer.ToString() &&
                cellTexts[4].text == currentPlayer.ToString() &&
                cellTexts[8].text == currentPlayer.ToString())
            {
                return true;
            }

            if (cellTexts[2].text == currentPlayer.ToString() &&
                cellTexts[4].text == currentPlayer.ToString() &&
                cellTexts[6].text == currentPlayer.ToString())
            {
                return true;
            }

            return false;
        }

        private bool CheckDraw()
        {
            foreach (var cellText in cellTexts.Values)
            {
                if (cellText.text == "")
                {
                    return false;
                }
            }
            return true;
        }

        public void RestartGame()
        {
            currentPlayer = 'X';
            isGameOver = false;
            messageText.text = "Починає гравець X";
            restartButton.gameObject.SetActive(false);

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].interactable = true;
                cellTexts[i].text = string.Empty;
            }
        }

        #endregion
    }
}
