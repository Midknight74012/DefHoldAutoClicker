I made this so I can make one of the most tedious tasks I have to do after testing is much faster and easier.


Having already used this, what used to take me up to several minutes to do the fail process now only takes seconds. Even with all the checkboxes checked, I think it takes less than 5 seconds to complete.

I also added a keydown feature to the serialNumberTextBox so when you press Enter, it will go through the whole process without having to press the Execute Button. The scanners we use send the Enter key after the barcode is scanned. Thinking on it now, I'll add a feature where if the Tab key is sent after scanning, it will do the same thing because our scanners can either Enter or Tab after it scans the barcode.

Also, when the process finishes, the application is activated and the text in the serialNumberTextBox is highlighted so all the user has to do is scan the next unit if it has the exact same fails.

I used a NuGet called InputSimulatorPlus so NetTerm can read the Down Arrow, F2, F3, and F6 keys when prompted.
