# Brute Force Password (WPF + AES Encryption)

## Overview

This project demonstrates a simple WPF application that uses AES encryption and a brute force approach to finding or validating a password. It includes two main components:

1. **Encryption.cs** – Contains methods for encrypting data (e.g., a password) using AES.
2. **MainWindow.xaml.cs** – The WPF window’s code-behind, which handles UI events (like button clicks and slider changes) and uses the `Encryption` class.

## Features

- **AES Encryption**: Demonstrates how to encrypt a string using AES.
- **Brute Force Simulation**: Includes logic to calculate the number of threads for brute forcing a password.
- **WPF UI**: Provides a simple interface with a slider for adjusting thread usage and a label to display progress (e.g., 25%, 50%, 75%, 100%).

## Project Structure


## How It Works

1. **AES Encryption**  
   - The `Encryption` class uses `Aes.Create()` to generate a cryptographic object.  
   - It converts the plain text into bytes and uses a key (`Key`) and initialization vector (`IV`) for encryption.
   - The method `EncryptStringToBytes_Aes` encrypts the input text and returns the encrypted byte array.

2. **Brute Force Simulation**  
   - When the button is clicked (`Button_Click` in `MainWindow.xaml.cs`), the code retrieves the user input (e.g., a password).
   - The application can simulate a brute force attack by iterating over possible passwords or using multiple threads.
   - The slider in the UI controls how many threads are used, which is calculated by `CalculateMaxThreads`.

3. **UI and Thread Feedback**  
   - A slider in the WPF window lets users choose a value (e.g., 0, 1, 2, 3, 4) which maps to different thread counts (`25%`, `50%`, `75%`, `100%`).
   - The label (`ThreadLabel`) updates to reflect the slider’s position.

## Getting Started

1. **Clone or Download**  
   - Clone the repository or download the project folder.

2. **Open in Visual Studio**  
   - Launch Visual Studio (2019 or later recommended).
   - Select **File > Open > Project/Solution** and navigate to the `.csproj` file.

3. **Build and Run**  
   - Press **F5** or choose **Debug > Start Debugging** to run the application.
   - The main window will open, allowing you to input text, adjust the slider, and test encryption or brute force logic.

## Customization

- **Adjust Encryption Settings**:  
  - Modify the key length or encryption mode in `Encryption.cs` to explore different AES configurations.
- **Change UI**:  
  - Update `MainWindow.xaml` to add new controls or change layout.
- **Extend Brute Force Logic**:  
  - Implement real password cracking logic by enumerating possible passwords.  
  - Improve thread handling or distribution for more realistic brute force scenarios.

## Limitations & Disclaimer

- This project is primarily for **educational purposes** to demonstrate AES encryption and brute force concepts.  
- Real-world password cracking is **ethically and legally** restricted; ensure you have permission before attempting such techniques.

## License

This project is available for educational and personal use.  
Feel free to modify and distribute under your preferred license terms.

## Author

- **[Mo Kenawi]** – Creator of the project.
