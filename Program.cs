/*
* Programme écrit en C# mais représente un programme C.
* Ceci implique par exemple, qu'on ne peut pas utiliser la propriété "length" sur un array. 
* Si on passe l'array a une fonction, il faut également passé un autre paramètre représentant 
* la taille de cet array.
*/
using System;

namespace algo
{
    class Program
    {
        static void Main(string[] args)
        {
            int arraySize = 11;
            int[] array = {3, 2, 1, 4, 10, 11, 12, 13, 14, 15, 0};

            if (array.Length != arraySize) 
            {
                throw new Exception($"Le tableau ne contient pas {arraySize} valeurs.");
            }

            var finder = new Finder();
            int result = finder.FindBiggestPlateIndex(array, arraySize);

            Console.WriteLine("********************************************************************************************");
            Console.WriteLine($"* L'indice de fin du plus grand plateau \"à {Constants.ValeurPresExact} valeurs près\" présent dans le tableau est : {result} *");
            Console.WriteLine("********************************************************************************************");
        }

        
    }

    public class Finder {
        /// <summary>
        /// Cette fonction retourne l'indice de fin du plus grand plateau "à x valeurs près"
        /// (donc par exemple un plateau de valeurs allant de 12 à 15) présent dans un tableau.
        /// </summary>
        /// <param name="array">Tableau d'entiers</param>
        /// <param name="arraySize">Taille du tableau d'entiers</param>
        /// <returns></returns>
        public int FindBiggestPlateIndex(int[] array, int arraySize) {
            int[] results = new int[arraySize];
            results[arraySize-1] = 1;
            for(int i = 0; i < arraySize - 1; i++) // arraySize - 1 car ca ne sert à rien de vérifier si la derniere valeur à un plateau plus grand que 1 puisque c'est impossible
            {
                int marker = array[i];
                int[] intermediaryResults = new int[Constants.ValeurPres];
                for (int j = 0; j < Constants.ValeurPres; j++)
                {                  
                    Console.WriteLine($"Valeur repère : {marker}");
                    intermediaryResults[j] = GetBiggestPlateByRange(marker, i, array, arraySize, GetRange(marker, j));
                }

                Console.WriteLine($"--- --- --- --- --- --- --- --- --- --- > Plus grand plateau trouvé parmis ces {Constants.ValeurPres} ranges: {GetBiggestInt(intermediaryResults, Constants.ValeurPres)}");
                results[i] = GetBiggestInt(intermediaryResults, Constants.ValeurPres);
            }

            Console.WriteLine("Résumé des plateaux trouvés par indice (indice de début du plateau)");
            Console.WriteLine("------------------------------------------------------------------");
            for (int i = 0; i < arraySize; i++)
            {
                Console.WriteLine($"Résultat pour l'indice {i}: {results[i]}");
            }

            return GetIndex(results, arraySize);
        }
        
        /// <summary>
        /// Retourne le plus grand plateau (à x valeur près) dans un tableau, à partir d'une valeur
        /// de départ, passée en paramètre.
        /// </summary>
        /// <param name="current">Valeur de départ</param>
        /// <param name="indexCurrent">Indice à laquelel se trouve la valeur dans le tableau</param>
        /// <param name="array">Tableau contenant les entiers dans lesquels on doit trouver le plateau</param>
        /// <param name="arraySize">Taille du tableau</param>
        /// <param name="range">Tableau contenant le range de valeur contre lesquel la valeur est testée</param>
        /// <returns></returns>
        public int GetBiggestPlateByRange(int current, int indexCurrent, int[] array, int arraySize, int[] range) 
        {
            if (indexCurrent < arraySize-1) 
            {
                indexCurrent++;
                int nextValue = array[indexCurrent];
                
                Console.WriteLine($"--- Valeur suivante dans le tableau: {nextValue}");

                if (
                    nextValue == range[0]
                    || nextValue == range[1] 
                    || nextValue == range[2]
                    || nextValue == range[3]) 
                {
                    return 1 + GetBiggestPlateByRange(current, indexCurrent, array, arraySize, range);
                } else {
                    return 1;
                } 
            } else {
                return 1;
            }
        }

        /// <summary>
        /// Retourne le range de valeur pour la recherche d'un plateau
        /// </summary>
        /// <param name="marker"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public int[] GetRange(int marker, int rank) {
            int markerRank = marker + rank;
            int[] results = new int[Constants.ValeurPres];

            Console.Write($"Compare à la rangée n°{rank + 1} :");
            for (int i = 0; i < Constants.ValeurPres ; i++) 
            {
                results[i] = markerRank - i;
                Console.Write($" {results[i]}");
            }
            Console.WriteLine();
            return results;
        }

        /// <summary>
        /// Retourne le plus grand entier trouvé dans un tableau d'entier
        /// </summary>
        /// <param name="array">Tableau d'entier</param>
        /// <param name="arraySize">Taille du tableau</param>
        /// <returns></returns>
        public int GetBiggestInt(int[] array, int arraySize) {
            var temp = Int32.MinValue;
            for (int i = 0; i < arraySize; i++)
            {
                if (array[i] > temp)
                {
                    temp = array[i];
                }
            }

            return temp;
        }

        /// <summary>
        /// Trouve la dernière occurence du plus grand entier présent dans un tableau et ensuite ajoute (indice - 1) à la valeur trouvée 
        /// pour pouvoir retourné l'indice de fin du plus grand plateau à x valeurs près.
        /// </summary>
        /// <param name="array">Tableau d'entier qui doit contenir les indices de début des plateaux</param>
        /// <param name="arraySize">Taille du tableau</param>
        /// <returns></returns>
        public int GetIndex(int[] array, int arraySize) {
            var temp = default(int);
            var index = default(int);
            for (int i = 0; i < arraySize; i++)
            {
                if (array[i] >= temp) // Ici on peut choisir si on veut retourner la premiere occurrence ou la derniere. Si on met ">" on garde la premiere occurence
                {
                    temp = array[i];
                    index = temp + i - 1;
                }
            }

            return index;
        }
    }

    public static class Constants 
    {
        public const int ValeurPres = 3 + 1; // "à 3 valeurs pres" donc la valeur elle même + 3
        public const int ValeurPresExact = 3;
    }
}
