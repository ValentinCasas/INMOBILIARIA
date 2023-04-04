-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 04-04-2023 a las 07:57:39
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
  `IdInquilino` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`Id`, `FechaInicio`, `FechaFinalizacion`, `MontoAlquilerMensual`, `Activo`, `IdInquilino`) VALUES
(15, '2023-04-02 00:00:00', '2023-05-02 00:00:00', '5000', 1, 23),
(16, '2023-04-03 00:00:00', '2023-04-27 00:00:00', '750', 0, 25);

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
  `IdPropietario` int(11) NOT NULL,
  `IdContrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`Id`, `Direccion`, `Uso`, `Tipo`, `CantidadAmbientes`, `Coordenadas`, `PrecioInmueble`, `Estado`, `IdPropietario`, `IdContrato`) VALUES
(12, 'Calle 10 de Octubre, #23, Santo Domingo, República', 'residencial', 'casa', 5, '40.748817, -73.985428', '5000', 'disponible', 12, 15),
(13, '123 Main St, Anytown, USA, 12345', 'comercial', 'local', 2, '-33.865143, 151.209900', '750', 'disponible', 26, 16);

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
(37, 56789432, 'Hector', 'Fuentes', 55667788, 'hector.fuentes@gmail.com'),
(38, 67890543, 'Florencia', 'Cruz', 66778899, 'florencia.cruz@gmail.com');

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
(4, 1, '2023-04-13 00:00:00', '5000', 15);

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

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInquilino` (`IdInquilino`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdPropietario` (`IdPropietario`,`IdContrato`),
  ADD KEY `IdContrato` (`IdContrato`);

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
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilino` (`id`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`IdContrato`) REFERENCES `contrato` (`Id`),
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
