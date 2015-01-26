ECHO OFF

ECHO deleting myStore...
rmdir myStore /S /Q

wlc addList Liste1 >temp.txt
set /p listId=<temp.txt
wlc addList Liste2
wlc showLists

wlc addTask %listId% Task1 >temp.txt
set /p taskId=<temp.txt
wlc showTasks %listId% Active

wlc showLists

ECHO deactivating task...
wlc deactivateTask %taskId%
wlc showTasks %listId% Active
pause