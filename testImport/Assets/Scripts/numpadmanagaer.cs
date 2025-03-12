using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numpadmanagaer : MonoBehaviour
{
    public ParticleSystem successParticles; // Assign in Inspector
    private List<int> targetNumber = new List<int>(); // Stores the 3-digit code
    private List<int> playerInput = new List<int>(); // Stores player input
    public TextMeshProUGUI tinput; // Displays player's input
    public TextMeshProUGUI tcode; // Displays the correct code

    void Start()
    {
        GenerateNewNumber();
        UpdatePlayerInputText(); // Clear input display on start
    }

    // Call this from Numpad when a number is pressed
    public void addnumber(int number)
    {
        playerInput.Add(number);
        Debug.Log("Number added: " + number);
        UpdatePlayerInputText(); // Update display of player's input

        if (playerInput.Count == 3)
        {
            CheckCode();
        }
    }

    // Generates a new random 3-digit code (digits 1 to 4)
    void GenerateNewNumber()
    {
        targetNumber.Clear();
        for (int i = 0; i < 3; i++)
        {
            targetNumber.Add(Random.Range(1, 5)); // 1 to 4 inclusive
        }
        
        // Convert targetNumber list to a string like "3 1 4"
        tcode.text = "Code: " + string.Join(" ", targetNumber);
        Debug.Log("New target code: " + targetNumber[0] + targetNumber[1] + targetNumber[2]);
    }

    // Check if player's input matches the target number
    void CheckCode()
    {
        bool correct = true;
        for (int i = 0; i < 3; i++)
        {
            if (playerInput[i] != targetNumber[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            Debug.Log("Correct code entered!");
            successParticles.Play(); // Play particle system
        }
        else
        {
            Debug.Log("Wrong code! Resetting input and generating new code.");
            GenerateNewNumber(); // Generate a new code on failure
        }

        playerInput.Clear(); // Reset input for next attempt
        UpdatePlayerInputText(); // Clear the input display
    }

    // Update the player's input display text
    void UpdatePlayerInputText()
    {
        if (playerInput.Count == 0)
        {
            tinput.text = "Input: ";
        }
        else
        {
            tinput.text = "Input: " + string.Join(" ", playerInput);
        }
    }
}


