# DOSBox Launcher

This is a very minimal front end for DOSBox.

It adds a menu option to the New menu in explorer to create a config file that automatically mounts the directory and runs the app.

## Install
Copy dosbox-launcher.exe to a folder that all users can read from and open it, then select option 1.

## Removal
Open dosbox-launcher.exe and choose option 2, select whether or not you want to remove the settings.

## Configuration
To change the path to DOSBox after it has been setup open dosbox-launcher.exe and choose option 3.

If you want to modify the dosbox template for new launcher files you can find it at %APPDATA%\DOSBox Launcher\default.dosbox.

If you do choose to modify the template, the following should remain in the autoexec section.

	mount c .
	c: