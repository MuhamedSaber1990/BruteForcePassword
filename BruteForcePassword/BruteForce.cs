using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;

namespace BruteForcePassword
{
    public class BruteForceAttack
    {
        private string? encryptedPass; // Field to store the encrypted password

        public static string GetResult(byte[] encryptedPass, byte[] encryptionKey, byte[] IV, int maxThreads)
        {
            var timeStarted = DateTime.Now; // Record the start time
            charactersToTestLength = charactersToTest.Length; // Get the length of characters to test
            var estimatedPasswordLength = 0;

            Password pass = new Password();
            string plainTextPass = pass.DecryptStringFromBytes_Aes(encryptedPass, encryptionKey, IV); // Decrypt the password
            while (!isMatched)
            {
                estimatedPasswordLength++;
                startBruteForce(estimatedPasswordLength, encryptedPass, encryptionKey, IV, plainTextPass, maxThreads); // Start the brute-force attack
            }
            var timeEnded = DateTime.Now; // Record the end time
            var timeTaken = timeEnded - timeStarted; // Calculate the time taken
            result += "\nTime taken: " + timeTaken.ToString(); // Append the time taken to the result
            return result; // Return the result
        }

        public static string result; // Field to store the result

        private static bool isMatched = false; // Flag to indicate if the password is matched
        private static int charactersToTestLength = 0; // Length of characters to test
        private static long computedKeys = 0; // Counter for computed keys

        private static char[] charactersToTest = // Array of characters to test
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
            'F','G','H','I','J','K','L','M','N','O','P','Q','R',
            'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
            '6','7','8','9','0','!','@','#','$','%','^','&','*',
            '(',')','_','+','-','=','[',']','{','}',',','|',';',
            ':','.','<','>','?','/'
        };

        private static void startBruteForce(int keyLength, byte[] encryptedPass, byte[] encryptionKey, byte[] IV, string plainTextPass, int maxThreads)
        {
            var threads = new List<Thread>(); // List to store threads
            int segmentSize = charactersToTestLength / maxThreads; // Calculate the segment size for each thread
            for (int i = 0; i < maxThreads; i++)
            {
                int start = i * segmentSize; // Start index for this segment
                int end = (i == maxThreads - 1) ? charactersToTestLength : start + segmentSize; // End index for this segment
                var thread = new Thread(() => createNewKey(0, createCharArray(keyLength, charactersToTest[start]), keyLength, keyLength - 1, plainTextPass, start, end)); // Create and start a new thread
                threads.Add(thread); // Add thread to the list
                thread.Start(); // Start the thread
            }

            foreach (var thread in threads)
            {
                thread.Join(); // Wait for all threads to complete
            }
        }

        private static char[] createCharArray(int length, char defaultChar)
        {
            return (from c in new char[length] select defaultChar).ToArray(); // Create and return an array of specified length initialized with defaultChar
        }

        private static void createNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string plainTextPass, int start, int end)
        {
            var nextCharPosition = currentCharPosition + 1; // Calculate next character position
            for (int i = start; i < end; i++)
            {
                keyChars[currentCharPosition] = charactersToTest[i]; // Set the current character

                if (currentCharPosition < indexOfLastChar)
                {
                    createNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar, plainTextPass, 0, charactersToTestLength); // Recursively create new keys
                }
                else
                {
                    computedKeys++; // Increment the computed keys counter
                    string possiblePass = new string(keyChars); // Create a possible password
                    if (possiblePass == plainTextPass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true; // Set isMatched to true if the password is matched
                            result = possiblePass; // Set the result
                        }
                        return; // Exit if matched
                    }
                }
            }
        }
    }
}
