<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../Styles/formulaire.css">
    <title>Document</title>
</head>
<body>
    <form action="../Controllers/confirmation.php" method="get">
        <label> Nom</label>
        <input type="text" name="nom" required>
        <label> Prénom</label>
        <input type="text" name="prenom" required>
        <label> Email</label>
        <input type="email" name="email" required>
      <input type="hidden" name="creneau_id" value="<?php echo htmlspecialchars($creneau_id); ?>">
    <button type="submit">Reserver</button>
    </form>
</body>
</html>