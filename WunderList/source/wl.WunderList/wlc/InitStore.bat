ECHO OFF

ECHO deleting myStore...
rmdir myStore /S /Q
rmdir myRmStore /S /Q
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

ECHO MOVING
wlc addTask %listId% Task2 >temp.txt
set /p taskId2=<temp.txt
wlc addTask %listId% Task3 >temp.txt
set /p taskId3=<temp.txt
ECHO show tasks...
wlc showTasks %listId% Active
ECHO move task...
wlc moveTask %taskId3% %taskId2% 
ECHO show tasks...
wlc showTasks %listId% Active
ECHO:

ECHO toggle importance
wlc toggleImportance %taskId3%
ECHO show tasks...
wlc showTasks %listId% Active
ECHO toggle back...
wlc toggleImportance %taskId3%
ECHO show tasks...
wlc showTasks %listId% Active
ECHO:

ECHO ACTIVATING / DEACTIVATING
wlc deactivateTask %taskId2%
wlc showTasks %listId% Active
wlc showLists
ECHO reactivating...
wlc activateTask %taskId2%
wlc showTasks %listId% Active
wlc showLists
ECHO:

pause