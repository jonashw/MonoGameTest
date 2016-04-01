SET width=250
SET /a width2 = 2 * %width%
SET /a width6 = 6 * %width%
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Ducking.png"                 --export-id=Ducking                 --export-width=%width%  Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Jumping.png"                 --export-id=Jumping                 --export-width=%width%  Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Sliding.png"                 --export-id=Sliding                 --export-width=%width%  Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Idle.png"                    --export-id=Idle                    --export-width=%width%  Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Running.png"                 --export-id=Running                 --export-width=%width6% Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-Cannonball.png"              --export-id=Cannonball              --export-width=%width%  Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-CannonballCrash.png"         --export-id=CannonballCrash         --export-width=%width2% Guy.svg
"C:\Program Files (x86)\Inkscape\inkscape.exe" --export-png="Guy-CannonballCrashRecovery.png" --export-id=CannonballCrashRecovery --export-width=%width6% Guy.svg
