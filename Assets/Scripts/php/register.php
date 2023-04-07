<?php

$login = $_POST["login"];
$password = $_POST["password"];

$mysqli = new mysqli('*', '*', '*', '*');

if($mysqli->mysqli_connect_error)
    die("Error connection database" . $mysqli->connection_status);

$query = "SELECT COUNT(*) as count FROM Users Where login = '$login'";
$result = $mysqli->query($query);
$row = $result->fetch_assoc();
if($row['count'] > 0)
    die("The username is already occupied, choose another username");

$passwordHash = password_hash($password, PASSWORD_DEFAULT);

$query = "INSERT INTO Users (login, password) VALUES ('$login', '$passwordHash')";

if($mysqli->query($query) === TRUE)
    echo "Users add to database";
else
    echo "database error: " . $mysqli->error; 

    $mysqli->close();
?>