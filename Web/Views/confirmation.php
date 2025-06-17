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
    <p>Vous avez réservé le créneau : <?php echo htmlspecialchars($creneau['starthour'] . ' à ' . $creneau['endhour']. 'le' . $creneau['day_id']); ?></p>
    <p>Pour toute question, veuillez contacter me contacter à cette adresse mail: </p> <a href="contact@e-ki-libre.net"></a>
    <p>Vous recevrez un e-mail avec toutes les informations utiles pour que votre rendez-vous se passe au mieux.</p>
    <p>Vous pouvez retourner à la page de réservation <a href="../Controllers/afficher_prestation.php">ici</a>.</p>
    <footer>
        <p>&copy; 2023 &kilibre. Tous droits réservés.</p>
        <div class="social-icons">
<<<<<<< HEAD
          
            <a href="#"><i class="fab fa-linkedin-in"></i></a>
=======
            <a href="https://www.linkedin.com/in/bertrand-mangin/"><i class="fab fa-linkedin-in"></i></a>
>>>>>>> 5f460573c831dff733aef7659f258d965fcfa6d9
        </div>
</body>
</html>