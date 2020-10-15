# dir-tree

My version of the Unix `tree` utility to show a tree view of a specified directory written in .NET 5.

Supports the following parameters:

* [Required] --path: Path to directory to start the tree view.
* [Optional] --max-depth [default: infinite]: Max directory depth to go to.
* [Optional] --only-directories [default: false]: Show only directories (i.e. do not show files).
* [Optional] --ordering: How to order files and folders. Has following options:
  * Alphabetically [default].
  * LastModification.
  * FileSize. Orders by file size, then alphabetically.
