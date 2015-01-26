ECHO OFF

ECHO deleting myStore...
rmdir myStore /S /Q
ECHO:

ECHO adding lists...
wlc addList Liste1 >temp.txt
set /p listId=<temp.txt
wlc addList Liste2
ECHO show lists...
wlc showLists
ECHO:

ECHO adding task...
wlc addTask %listId% Task1 >temp.txt
set /p taskId=<temp.txt
ECHO show tasks...
wlc showTasks %listId% Active
ECHO show lists...
wlc showLists
ECHO:

ECHO deactivating task...
wlc deactivateTask %taskId%
ECHO show tasks with Active...
wlc showTasks %listId% Active
ECHO show tasks with Inative...
wlc showTasks %listId% Inactive
ECHO show lists...
wlc showLists
ECHO:


ECHO:

pause