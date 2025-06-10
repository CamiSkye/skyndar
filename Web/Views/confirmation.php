<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1>Confirmation de la réservation</h1>
    <p>Merci, votre réservation a été confirmée.</p>
    <p>Vous avez réservé le créneau : <?php echo htmlspecialchars($creneau['starthour'] . ' à ' . $creneau['endhour']); ?></p>
    <p>Pour toute question, veuillez contacter Bertrand Mangin.</p>
    <p>Vous pouvez retourner à la page de réservation <a href="../Controllers/afficher_prestation.php">ici</a>.</p>
    <footer>
        <p>&copy; 2023 &kilibre. Tous droits réservés.</p>
        <div class="social-icons">
            <a href="#"><i class="fab fa-facebook-f"></i></a>
            <a href="#"><i class="fab fa-twitter"></i></a>
            <a href="#"><i class="fab fa-instagram"></i></a>
            <a href="#"><i class="fab fa-linkedin-in"></i></a>
        </div>
</body>
</html>