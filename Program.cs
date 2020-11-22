/*
* Programme écrit en C# mais représente un programme C.
* Ceci implique par exemple, qu'on ne peut pas utiliser la propriété "Length" sur un array. 
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
            int i, temp, index, j, tempBiggestPlate, result;
            i = temp = index = j = tempBiggestPlate = result = default(int);
            int VALEURPRES = Constants.ValeurPres;
            while (i < arraySize - 1) { // arraySize - 1 car ca ne sert à rien de vérifier si la derniere valeur à un plateau plus grand que 1 puisque c'est impossible
                while(j < VALEURPRES) {
                    result = GetBiggestPlateByRange(array[i], i, array, arraySize, j);
                    if (result > tempBiggestPlate) {
                        tempBiggestPlate = result;
                    }
                    j++;
                }

                if (tempBiggestPlate >= temp) { // ici on peut choisir si on veut retourner la premiere occurrence ou la derniere. Si on met ">" on garde la premiere occurence
                    temp = tempBiggestPlate;
                    index = tempBiggestPlate + i;
                }
                i++;   
            }

            // Console.WriteLine("Résumé des plateaux trouvés par indice (indice de début du plateau)");
            // Console.WriteLine("------------------------------------------------------------------");
            // for (int i = 0; i < arraySize; i++)
            // {
            //     Console.WriteLine($"Résultat pour l'indice {i}: {results[i]}");
            // }

            return index;
        }
        
        /// <summary>
        /// Retourne le plus grand plateau (à x valeur près) présent dans un tableau, à partir d'une valeur
        /// de départ, passée en paramètre.
        /// </summary>
        /// <param name="current">Valeur de départ</param>
        /// <param name="indexCurrent">Indice à laquelel se trouve la valeur dans le tableau</param>
        /// <param name="array">Tableau contenant les entiers dans lesquels on doit trouver le plateau</param>
        /// <param name="arraySize">Taille du tableau</param>
        /// <param name="range">Tableau contenant le range de valeur contre lesquel la valeur est testée</param>
        /// <returns></returns>
        public int GetBiggestPlateByRange(int current, int indexCurrent, int[] array, int arraySize, int rank) 
        {
            if (indexCurrent < arraySize-1) 
            {
                indexCurrent++;
                int nextValue = array[indexCurrent];
                //Console.WriteLine($"--- Valeur suivante dans le tableau: {nextValue}");

                if (
                    nextValue == current + rank
                    || nextValue == current + rank - 1 
                    || nextValue == current + rank - 2
                    || nextValue == current + rank - 3) 
                {
                    return 1 + GetBiggestPlateByRange(current, indexCurrent, array, arraySize, rank);
                } else {
                    return 0;
                } 
            } else {
                return 0;
            }
        }
    }

    public static class Constants 
    {
        public const int ValeurPres = 3 + 1; // "à 3 valeurs pres" donc la valeur elle même + 3
        public const int ValeurPresExact = 3;
    }
}