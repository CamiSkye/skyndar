<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <form action="../Controllers/confirmation.php" method="get">
        <label> nom</label>
        <input type="text" name="nom" required>
        <label> prénom</label>
        <input type="text" name="prenom" required>
        <label> email</label>
        <input type="email" name="email" required>
      <input type="hidden" name="creneau_id" value="<?php echo htmlspecialchars($creneau_id); ?>">
    <button type="submit">Reserver</button>
    </form>
</body>
</html>