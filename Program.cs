/*
* Programme écrit en C# mais représente un programme C.
* Pour être au plus proche des spécifications (environnement, ...), ceci implique par exemple, 
* qu'on ne peut pas utiliser la propriété "length" sur un array. Si on passe l'array a une fonction,
* il faut également passé un autre paramètre représentant la taille de cet array.
*/
using System;

namespace algo
{
    class Program
    {
        static void Main(string[] args)
        {
            // int arraySize = 11;
            // int[] array = {7, 12, 15, 3, 0, 8, 9, 12, 10, 11, 3};

            //int arraySize = 11;
            //int[] array = {1, 2, 3, 4, 2, 5, 2, 3, 4, 0, 13};
            int valeurPres = 3; // devrait être utiliser dans GetMatrix
            int arraySize = 11;
            int[] array = {3, 2, 1, 4, 10, 11, 12, 13, 14, 15, 0};

            if (array.Length != arraySize) 
            {
                throw new Exception($"Le tableau ne contient pas {arraySize} valeurs.");
            }

            var finder = new Finder();
            int result = finder.FindBiggestPlateIndex(array, arraySize);

            Console.WriteLine(result);
        }

        
    }

    public class Finder {
        /// <summary>
        /// Cette fonction retourne l'indice de fin du plus grand plateau "à trois valeurs près"
        /// (donc par exemple un plateau de valeurs allant de 12 à 15) présent dans un tableau.
        /// </summary>
        /// <param name="array">Tableau d'entiers</param>
        /// <param name="arraySize">Taille du tableau d'entiers</param>
        /// <returns></returns>
        public int FindBiggestPlateIndex(int[] array, int arraySize) {
            //int biggestPlateIndex = default(int);
            int[] results = new int[arraySize];
            for(int i = 0; i < arraySize; i++) 
            {
                int marker = array[i];
                // for(int j = i+1; j < arraySize; j++) 
                // {
                //     Console.WriteLine($"Valeur suivante : {array[j]}");
                //     GetBiggestPlateIndex();
                // }
                int[,] matrix = GetMatrix(marker);
                Console.WriteLine("{0, -6} {1, -4} {2, -5} {3, -5} {4, -5} {5, -5}",
                    "Marker", "Next", "R1", "R2", "R3", "R4");
                int[] rangeResults = new int[4];// 4 parce que 3 valeurs pres
                results[i] = GetBiggestPlateIndex(marker, i, default(int), array, arraySize, matrix, rangeResults);
                var test = "lol";
            }

            for (int i = 0; i < arraySize; i++)
            {
                Console.WriteLine($"Résultat : {results[i]}");
            }

            return default(int);
        }
        
        /// <summary>
        /// Retourne le plus grand plateau (à 3 valeur près) dans un tableau, à partir d'une valeur
        /// de départ, passée en paramètre.
        /// </summary>
        /// <param name="current">Valeur de départ</param>
        /// <param name="indexCurrent">Indice à laquelel se trouve la valeur dans le tableau</param>
        /// <param name="array">Tableau contenant les entiers dans lesquels on doit trouver le plateau</param>
        /// <param name="array">Taille du tableau</param>
        /// <param name="matric">Tableau à deux dimensions contenant les différents range possible pour une valeur</param>
        /// <param name="init">Quand init est true, on sauvegarde des infos</param>
        /// <returns></returns>
        public int GetBiggestPlateIndex(int current, int indexCurrent, int indexNext, int[] array, int arraySize, int[,] matrix, int[] results) 
        {

            //Console.WriteLine($"Paramètre à l'entrée de la fonction : current = {current}, indexCurrent = {indexCurrent}, arraySize = {arraySize}");
            
            //int indexCurrentSaved = -1;
            // if (indexCurrentSaved < 0) {
            //     indexCurrentSaved = indexCurrent;
            // }



            


            if (indexNext == 0) {
                indexNext = indexCurrent;
            }

            if (indexNext == arraySize - 1) 
            {
                return indexNext; 
            }

            Console.WriteLine($"Valeur repère : {current}");

            int nextIndex = indexNext + 1;
            int nextValue = array[nextIndex];
            int outOfRangeValue = current -4; 
            
            Console.WriteLine($"--- Valeur suivante : {nextValue}");
            
            int biggestIndexR1 = default(int);
            int biggestIndexR2 = default(int);
            int biggestIndexR3 = default(int);
            int biggestIndexR4 = default(int);

            if (
                nextValue == matrix[0, 0]
                || nextValue == matrix[1, 0] 
                || nextValue == matrix[2, 0]
                || nextValue == matrix[3, 0]) 
            {

                Console.WriteLine($"--- --- R1 : true");


                // Lors de la récursion suivante, on s'assure que la fonction n'ira plus loin que pour 
                // ce range de valeur en mettant hors-jeu les autres valeurs.
                int[,] R1 = 
                {
                    { matrix[0, 0], outOfRangeValue, outOfRangeValue, outOfRangeValue },
                    { matrix[1, 0], outOfRangeValue, outOfRangeValue, outOfRangeValue },
                    { matrix[2, 0], outOfRangeValue, outOfRangeValue, outOfRangeValue },
                    { matrix[3, 0], outOfRangeValue, outOfRangeValue, outOfRangeValue },
                };
                results[0] = indexCurrent;
                _ = GetBiggestPlateIndex(current, indexCurrent, nextIndex, array, arraySize, R1, results);
                nextValue = array[indexCurrent+1];
                matrix = GetMatrix(current);
                nextIndex = 1;
                Console.WriteLine("*** INIT ***");
                Console.WriteLine($"Valeur repère : {current}");
                Console.WriteLine($"--- Valeur suivante : {nextValue}");
            } 

            // indexCurrentsaved doit toujours être > 0
            // indexCurrent = indexCurrentSaved;
            // nextValue = indexCurrent + 1;
            // matrix = GetMatrix(current);

            if (
                nextValue == matrix[0, 1]
                || nextValue == matrix[1, 1] 
                || nextValue == matrix[2, 1]
                || nextValue == matrix[3, 1])
            {
                Console.WriteLine($"--- --- R2 : true");

                // Lors de la récursion suivante, on s'assure que la fonction n'ira plus loin que pour 
                // ce range de valeur en mettant hors-jeu les autres valeurs.
                int[,] R2 = 
                {
                    { outOfRangeValue, matrix[0, 1], outOfRangeValue, outOfRangeValue },
                    { outOfRangeValue, matrix[1, 1], outOfRangeValue, outOfRangeValue },
                    { outOfRangeValue, matrix[2, 1], outOfRangeValue, outOfRangeValue },
                    { outOfRangeValue, matrix[3, 1], outOfRangeValue, outOfRangeValue },
                };
                results[1] = indexCurrent;
                _ = GetBiggestPlateIndex(current, indexCurrent, nextIndex, array, arraySize, R2, results);
                nextValue = array[indexCurrent+1];
                matrix = GetMatrix(current);
                nextIndex = 1;
                Console.WriteLine("*** INIT ***");
                Console.WriteLine($"Valeur repère : {current}");
                Console.WriteLine($"--- Valeur suivante : {nextValue}");
            } 
            
            if (
                nextValue == matrix[0, 2]
                || nextValue == matrix[1, 2] 
                || nextValue == matrix[2, 2]
                || nextValue == matrix[3, 2])
            {
                // Lors de la récursion suivante, on s'assure que la fonction n'ira plus loin que pour 
                // ce range de valeur en mettant hors-jeu les autres valeurs.
                int[,] R3 = 
                {
                    { outOfRangeValue, outOfRangeValue, matrix[0, 2], outOfRangeValue },
                    { outOfRangeValue, outOfRangeValue, matrix[1, 2], outOfRangeValue },
                    { outOfRangeValue, outOfRangeValue, matrix[2, 2], outOfRangeValue },
                    { outOfRangeValue, outOfRangeValue, matrix[3, 2], outOfRangeValue },
                };
                results[2] = indexCurrent;
                _ = GetBiggestPlateIndex(current, indexCurrent, nextIndex, array, arraySize, R3, results);
            } 
            
            if (
                nextValue == matrix[0, 3]
                || nextValue == matrix[1, 3] 
                || nextValue == matrix[2, 3]
                || nextValue == matrix[3, 3])
            {
                // Lors de la récursion suivante, on s'assure que la fonction n'ira plus loin que pour 
                // ce range de valeur en mettant hors-jeu les autres valeurs.
                int[,] R4 = 
                {
                    { outOfRangeValue, outOfRangeValue, outOfRangeValue, matrix[0, 2] },
                    { outOfRangeValue, outOfRangeValue, outOfRangeValue, matrix[1, 2] },
                    { outOfRangeValue, outOfRangeValue, outOfRangeValue, matrix[2, 2] },
                    { outOfRangeValue, outOfRangeValue, outOfRangeValue, matrix[3, 2] },
                };
                results[3] = indexCurrent;
                _ = GetBiggestPlateIndex(current, indexCurrent, nextIndex, array, arraySize, R4, results);
                indexNext = 0;
            }


            if (indexNext == 0) { // quand indexNext égal 0, on sait qu'on est revenu au premier-dernier tour de fonction
                int temp = Int32.MinValue;
                for (int i = 0; i < 4; i++) // 4 parce que à 3 valeurs près
                {
                    if (results[i] > temp) 
                    {
                        temp = results[i];
                    }
                }
                return temp;
            }

            //DO SOMETHING

            return indexNext;
        }

        /// <summary>
        /// Produit un tableau à deux dimensions contenant les différents range possible pour 
        /// une valeur.
        /// </summary>
        /// <param name="marker">Valeur repère pour laquel on veut connaitre les différents ranges possibles</param>
        public int[,] GetMatrix(int marker) 
        {
            // 4 parce que à 3 valeurs pres
            int[,] array2D = new int[4, 4] 
            {
                {marker, marker+1, marker+2, marker+3},
                {marker-1, marker, marker+1, marker+2},
                {marker-2, marker-1, marker, marker+1},
                {marker-3, marker-2, marker-1, marker},
            };

            return array2D;
        }
    }
}
