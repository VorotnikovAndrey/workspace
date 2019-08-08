using UnityEngine;
using UnityEngine.Assertions;
using UniverseTeam.Libraries;
using UniverseTeam.Utils;

namespace UniverseTeam.Core
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] private BlockParams blockPref;
        [SerializeField] private Transform finishPref;
        [SerializeField] private Transform playingField;
        [SerializeField] private float offsetBetweenObstacles = 20;
        [SerializeField] private int numberOfObstacles = 20;

        private float lastFigureSpawnPositionZ;

        private FiguresLibrary FiguresLibrary => GameManager.instance.figuresLibrary;

        private void Awake()
        {
            Assert.IsNotNull(blockPref);
            Assert.IsNotNull(playingField);
        }

        public void Init()
        {
            for (var i = 0; i < numberOfObstacles; i++)
            {
                GenerateObstruction(GetSpawnPosition());
            }

            GenerateFinish();
        }

        private void GenerateFinish()
        {
            var finish = Instantiate(finishPref);
            var spawnPosition = playingField.transform.position;

            spawnPosition.z = offsetBetweenObstacles + lastFigureSpawnPositionZ;
            finish.transform.position = spawnPosition;

            SessionManager.instance.SetFinishTransform(finish);
        }

        private Vector3 GetSpawnPosition()
        {
            var spawnPosition = playingField.transform.position;
            var lossyScale = blockPref.transform.lossyScale;

            spawnPosition.x -= lossyScale.x * FiguresLibrary.SymbolPerLine / 2 - lossyScale.x / 2;
            spawnPosition.y += lossyScale.y / 2;
            spawnPosition.z += offsetBetweenObstacles;

            return spawnPosition;
        }

        private void GenerateObstruction(Vector3 originPoint)
        {
            var figure = FiguresLibrary.Exaples.GetRandom();
            var symbolPerLine = FiguresLibrary.SymbolPerLine;
            var figureHeight = figure.Length / symbolPerLine;

            originPoint.z += lastFigureSpawnPositionZ;
            lastFigureSpawnPositionZ = originPoint.z + figureHeight;

            for (var i = 0; i < figureHeight; i++)
            {
                for (var y = 0; y < symbolPerLine; y++)
                {
                    var block = Instantiate(blockPref, originPoint + (Vector3.forward * i + Vector3.right * y), Quaternion.identity);

                    if (figure[y + (symbolPerLine + 1) * i] != '*')
                    {
                        block.MeshRenderer.material.color = Color.red;
                    }
                }
            }
        }
    }
}
