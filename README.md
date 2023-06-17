# AtomDB
## High Speed DataBase System

AtomDB (originally developped for AvRate's Master Bot by me) is a high speed database system.

# Usage
Load DataBase:
```cs
AtomManager.Initialize();
if(AtomManager.UseMultiThreaded) {
  var db = AtomMT.Load(".db");
}else {
  var db = Atom.Load(".db");
}
```

Save DataBase:
```cs
db.Save(".db);
```

Set / Get functions are self-explanatory.
