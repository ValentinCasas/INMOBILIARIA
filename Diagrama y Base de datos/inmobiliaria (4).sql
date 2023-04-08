-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 08-04-2023 a las 05:33:51
-- Versión del servidor: 10.4.24-MariaDB
-- Versión de PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `Id` int(11) NOT NULL,
  `FechaInicio` datetime NOT NULL,
  `FechaFinalizacion` datetime NOT NULL,
  `MontoAlquilerMensual` decimal(10,0) NOT NULL,
  `Activo` tinyint(1) NOT NULL,
  `IdInquilino` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`Id`, `FechaInicio`, `FechaFinalizacion`, `MontoAlquilerMensual`, `Activo`, `IdInquilino`, `IdInmueble`) VALUES
(18, '2023-04-08 00:00:00', '2023-04-28 00:00:00', '5000', 1, 16, 15),
(19, '2023-04-21 00:00:00', '2023-04-25 00:00:00', '1000', 1, 23, 14);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `Id` int(11) NOT NULL,
  `Direccion` varchar(50) NOT NULL,
  `Uso` varchar(50) NOT NULL,
  `Tipo` varchar(50) NOT NULL,
  `CantidadAmbientes` int(11) NOT NULL,
  `Coordenadas` varchar(100) NOT NULL,
  `PrecioInmueble` decimal(10,0) NOT NULL,
  `Estado` varchar(20) NOT NULL,
  `IdPropietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`Id`, `Direccion`, `Uso`, `Tipo`, `CantidadAmbientes`, `Coordenadas`, `PrecioInmueble`, `Estado`, `IdPropietario`) VALUES
(14, 'los quebrachos', 'comercial', 'local', 3, '123123123', '5000', 'disponible', 16),
(15, 'la punta', 'residencial', 'casa', 5, '122332', '2000', 'disponible', 13);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `Id` int(11) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellido` varchar(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`Id`, `Dni`, `Nombre`, `Apellido`, `Telefono`, `Email`) VALUES
(9, 11111111, 'Juan', 'García', 555, 'juangarcia@example.com'),
(10, 22222222, 'María', 'Rodríguez', 555, 'mariarodriguez@example.com'),
(11, 33333333, 'Carlos', 'Gómez', 555, 'carlosgomez@example.com'),
(12, 44444444, 'Ana', 'López', 555, 'analopez@example.com'),
(13, 55555555, 'Pedro', 'Martínez', 555, 'pedromartinez@example.com'),
(14, 66666666, 'Laura', 'Sánchez', 555, 'laurasanchez@example.com'),
(15, 77777777, 'Jorge', 'Fernández', 555, 'jorgefernandez@example.com'),
(16, 88888888, 'Lucía', 'Pérez', 555, 'luciaperez@example.com'),
(17, 99999999, 'Diego', 'Alvarez', 555, 'diegoalvarez@example.com'),
(18, 10101010, 'Sofía', 'González', 555, 'sofiagonzalez@example.com'),
(19, 20202020, 'Federico', 'Romero', 555, 'federicoromero@example.com'),
(20, 30303030, 'Valentina', 'Díaz', 555, 'valentinadiaz@example.com'),
(21, 40404040, 'Andrés', 'Hernández', 555, 'andreshernandez@example.com'),
(22, 50505050, 'Julia', 'Torres', 555, 'juliatorres@example.com'),
(23, 60606060, 'Miguel', 'Gutiérrez', 555, 'miguelgutierrez@example.com'),
(24, 12345678, 'Juan', 'Perez', 11223344, 'juan.perez@gmail.com'),
(25, 23456789, 'Maria', 'Gonzalez', 22334455, 'maria.gonzalez@gmail.com'),
(26, 34567890, 'Pedro', 'Fernandez', 33445566, 'pedro.fernandez@gmail.com'),
(27, 45678901, 'Lucia', 'Lopez', 44556677, 'lucia.lopez@gmail.com'),
(28, 56789012, 'Carlos', 'Garcia', 55667788, 'carlos.garcia@gmail.com'),
(29, 67890123, 'Luisa', 'Martinez', 66778899, 'luisa.martinez@gmail.com'),
(30, 78901234, 'Daniel', 'Rodriguez', 77889900, 'daniel.rodriguez@gmail.com'),
(31, 89012345, 'Laura', 'Sanchez', 89001122, 'laura.sanchez@gmail.com'),
(32, 90123456, 'Alejandro', 'Gomez', 90112233, 'alejandro.gomez@gmail.com'),
(33, 12345098, 'Julia', 'Torres', 11223344, 'julia.torres@gmail.com'),
(34, 23456109, 'Diego', 'Gutierrez', 22334455, 'diego.gutierrez@gmail.com'),
(35, 34567210, 'Ana', 'Diaz', 33445566, 'ana.diaz@gmail.com'),
(36, 45678321, 'Sofia', 'Alvarez', 44556677, 'sofia.alvarez@gmail.com'),
(37, 56789432, 'Hector', 'Fuentes', 55667788, 'hector.fuentes@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `Id` int(11) NOT NULL,
  `NumDePago` int(11) NOT NULL,
  `FechaDePago` datetime NOT NULL,
  `Importe` decimal(10,0) NOT NULL,
  `IdContrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`Id`, `NumDePago`, `FechaDePago`, `Importe`, `IdContrato`) VALUES
(7, 1, '2023-04-07 00:00:00', '5000', 18),
(8, 2, '2023-04-07 00:00:00', '5000', 18),
(9, 1, '2023-04-07 00:00:00', '1000', 19),
(11, 3, '2023-04-07 00:00:00', '5000', 18),
(12, 2, '2023-04-07 00:00:00', '1000', 19);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `Id` int(11) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellido` varchar(20) NOT NULL,
  `Telefono` bigint(20) NOT NULL,
  `Email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`Id`, `Dni`, `Nombre`, `Apellido`, `Telefono`, `Email`) VALUES
(12, 111111, 'Gabriel', 'Garcia', 11223344, 'gabriel.garcia@gmail.com'),
(13, 22222222, 'Laura', 'Ramirez', 22334455, 'laura.ramirez@gmail.com'),
(15, 44444444, 'Lucas', 'Vega', 44556677, 'lucas.vega@gmail.com'),
(16, 55555555, 'Carolina', 'Lopez', 55667788, 'carolina.lopez@gmail.com'),
(17, 66666666, 'Javier', 'Mendez', 66778899, 'javier.mendez@gmail.com'),
(18, 77777777, 'Natalia', 'Gonzalez', 77889900, 'natalia.gonzalez@gmail.com'),
(19, 88888888, 'Roberto', 'Diaz', 89001122, 'roberto.diaz@gmail.com'),
(20, 99999999, 'Agustina', 'Fernandez', 90112233, 'agustina.fernandez@gmail.com'),
(21, 10101010, 'Ricardo', 'Alvarez', 11223344, 'ricardo.alvarez@gmail.com'),
(22, 11112222, 'Valeria', 'Gomez', 22334455, 'valeria.gomez@gmail.com'),
(23, 22223333, 'Julian', 'Torres', 33445566, 'julian.torres@gmail.com'),
(24, 33334444, 'Alicia', 'Rodriguez', 44556677, 'alicia.rodriguez@gmail.com'),
(26, 55556666, 'Cecilia', 'Sanchez', 66778899, 'cecilia.sanchez@gmail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellido` varchar(20) NOT NULL,
  `Edad` int(11) NOT NULL,
  `Dni` bigint(20) NOT NULL,
  `Email` varchar(70) NOT NULL,
  `Clave` varchar(260) NOT NULL,
  `AvatarUrl` varchar(260) NOT NULL,
  `Rol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`Id`, `Nombre`, `Apellido`, `Edad`, `Dni`, `Email`, `Clave`, `AvatarUrl`, `Rol`) VALUES
(37, 'Mariano', 'Luzza Bonilla', 20, 22222222, 'marianoLuzza@gmail.com', 'wDEeOf0m4GHd71HfVwP7XEV1pUgiW2ymJvMwtk/F0GI=', '/Uploads\\avatar_d004435cfc6e4401a7a3fa5721b02840.jpg', 2),
(38, 'Maria', 'Laura', 30, 34000000, 'mariaLaura@gmail.com', 'vZlEtHizkfNSyogT4zTsFwLEB9DX0ZzRho4EvRqb6Y8=', '/Uploads\\avatar_13f742dadf214c29b4445a293a869e7e.jpg', 3),
(39, 'Kevin', 'Garcia', 21, 41222333, 'kevinGarcia12@gmail.com', 'Qn+o5tUUVufr6kzFoB2ObdSl0UvnzhnNWDH6o2nYQw0=', '/Imagenes/avatar_por_defecto.jpg', 3),
(40, 'Ivan', 'Collado', 35, 32444444, 'ivanCollado@gmail.com', 'zEgX/k8gU+/CtoZ0E8eSteIaRVwfZI5NDlnmD+K0I6U=', '/Uploads\\avatar_58a3a29a7ae54f76a49e75985dc03e6a.jpg', 3),
(41, 'Pia', 'Lucero', 20, 44234234, 'piaLucero@gmail.com', '73RA559NytJz3RDfwea02Y4xt1oJP5Wpz4QMCbJ2gS4=', '/Uploads\\avatar_248df2d23e06436685c6d64f50f2bb91.jpg', 2),
(42, 'Francisco', 'Messi', 20, 44354345, 'franciscoMessi@gmail.com', 'BeZNmaQJXapAV09AR7Une5eMGINbZc7uWvekEC5xflE=', '/Uploads\\avatar_7d9ed87c4e0a4f1bb0a1033717cf17aa.jpg', 3);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInquilino` (`IdInquilino`),
  ADD KEY `IdInmueble` (`IdInmueble`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdPropietario` (`IdPropietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdContrato` (`IdContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilino` (`id`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`IdInmueble`) REFERENCES `inmueble` (`Id`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`IdPropietario`) REFERENCES `propietario` (`id`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`IdContrato`) REFERENCES `contrato` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
