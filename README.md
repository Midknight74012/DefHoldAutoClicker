I made this so I can make one of the most tedious tasks I have to do after testing is much faster and easier.

I have to use NetTerm to place the units I test into Defective Hold, followed by Test Complete. There are 2 other screens I'd like for this to work with as well but at this time, I have yet to figure out how to do that.

The next thing I'd like to do with this application is to have settings form, config file, or ini file so I can change the DownArrow(int) values for if the NetTerm Defective Hold screen is changed again. It is on lines 138, 144, 151, 158, 164, 170, 176, and 182. I'd also like a way to detect the XY coordinates of the mouse cursor so the user can assign the coordinates of the Defective Hold Screen and Test Complete Screen if they do not like the way it currently is.

Having already used this, what used to take me up to several minutes to do the fail process now only takes seconds. Even with all the checkboxes checked, I think it takes less than 5 seconds to complete.

I also added a keydown feature to the serialNumberTextBox so when you press Enter, it will go through the whole process without having to press the Execute Button. The scanners we use send the Enter key after the barcode is scanned. Thinking on it now, I'll add a feature where if the Tab key is sent after scanning, it will do the same thing because our scanners can either Enter or Tab after it scans the barcode.

Also, when the process finishes, the application is activated and the text in the serialNumberTextBox is highlighted so all the user has to do is scan the next unit if it has the exact same fails.

I used a NuGet called InputSimulatorPlus so NetTerm can read the Down Arrow, F2, F3, and F6 keys when prompted.
