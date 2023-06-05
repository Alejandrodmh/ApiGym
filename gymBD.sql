-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 05-06-2023 a las 10:09:48
-- Versión del servidor: 8.0.31
-- Versión de PHP: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `gym`
--

DELIMITER $$
--
-- Procedimientos
--
DROP PROCEDURE IF EXISTS `deleteComentario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteComentario` (IN `id` INT)   delete from comentario where id_comentario=id$$

DROP PROCEDURE IF EXISTS `deleteCompra`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteCompra` (IN `id` INT)   BEGIN
delete from detalle_compra where id_compra=id;
delete from compra where id_compra=id;
END$$

DROP PROCEDURE IF EXISTS `deleteProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteProducto` (IN `id` INT)   delete from producto where id_producto=id$$

DROP PROCEDURE IF EXISTS `getComentarios`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getComentarios` ()   select * from comentario$$

DROP PROCEDURE IF EXISTS `getCompras`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getCompras` ()   select c.id_compra,p.nombre,c.fecha_compra,c.precio FROM
compra c,detalle_compra d,producto p WHERE
c.id_compra=d.id_compra and d.id_producto=p.id_producto$$

DROP PROCEDURE IF EXISTS `getProductos`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getProductos` ()   select * from producto$$

DROP PROCEDURE IF EXISTS `getUsuarios`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `getUsuarios` ()   select * from usuario$$

DROP PROCEDURE IF EXISTS `postComentario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `postComentario` (IN `id` INT, IN `p_contenido` VARCHAR(255))   update comentario set contenido=p_contenido WHERE
id_comentario=id$$

DROP PROCEDURE IF EXISTS `postProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `postProducto` (IN `id` INT, IN `nom` VARCHAR(100), IN `descr` VARCHAR(255), IN `prec` FLOAT)   update producto set nombre=nom,descripcion=descr,precio=prec WHERE
id_producto=id$$

DROP PROCEDURE IF EXISTS `putComentario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `putComentario` (IN `p_id_producto` INT, IN `p_contenido` VARCHAR(255), IN `p_id_usuario` INT)   insert into comentario (id_producto,contenido,fecha_comentario,id_usuario) 
values (p_id_producto,p_contenido,CURRENT_DATE(),p_id_usuario)$$

DROP PROCEDURE IF EXISTS `putCompra`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `putCompra` (IN `p_id_producto` INT, IN `p_cantidad` INT, IN `p_id_compra` INT, IN `p_id_usuario` INT)   BEGIN
    DECLARE v_precio DECIMAL(10, 2);
    DECLARE v_id_compra INT;
    
    -- Verificar si la compra existe
    SELECT id_compra INTO v_id_compra FROM compra WHERE id_compra = p_id_compra;

    -- Si la compra no existe, insertar una nueva compra
    IF v_id_compra IS NULL THEN
        SELECT precio * p_cantidad INTO v_precio FROM producto WHERE id_producto = p_id_producto;
        INSERT INTO compra (id_compra, fecha_compra, precio,id_usuario) VALUES (p_id_compra, CURRENT_DATE(), v_precio,p_id_usuario);
    END IF;

    -- Insertar el detalle de compra
    INSERT INTO detalle_compra (id_compra, id_producto, cantidad) VALUES (p_id_compra, p_id_producto, p_cantidad);
    UPDATE producto set cantidad=(cantidad-p_cantidad) where id_producto=p_id_producto;

    -- Si la compra existe, actualizar el precio_total de la compra
    IF v_id_compra IS NOT NULL THEN
        UPDATE compra SET precio = precio + (SELECT precio * p_cantidad FROM producto WHERE id_producto = p_id_producto) WHERE id_compra = p_id_compra;
    END IF;
END$$

DROP PROCEDURE IF EXISTS `putProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `putProducto` (IN `nom` VARCHAR(100), IN `descr` VARCHAR(255), IN `prec` FLOAT, IN `img` VARCHAR(100))   insert into producto (nombre,descripcion,precio,imagen) 
values (nom,descr,prec,img)$$

DROP PROCEDURE IF EXISTS `putUsuario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `putUsuario` (IN `p_nombre` VARCHAR(100), IN `p_email` VARCHAR(100), IN `p_contrasena` VARCHAR(100))   insert into usuario (nombre,email,contrasena) VALUES
(p_nombre,p_email,p_contrasena)$$

DROP PROCEDURE IF EXISTS `verificar`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `verificar` (IN `p_email` VARCHAR(100))   select COUNT(*) from usuario where email=p_email$$

DROP PROCEDURE IF EXISTS `VerificarEmail`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `VerificarEmail` (IN `p_email` VARCHAR(100))   select COUNT(*) from usuario where email= p_email$$

DROP PROCEDURE IF EXISTS `VerificarLogin`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `VerificarLogin` (IN `p_email` VARCHAR(100), IN `p_contrasena` VARCHAR(100))   select * from usuario where email=p_email and contrasena=p_contrasena$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `comentario`
--

DROP TABLE IF EXISTS `comentario`;
CREATE TABLE IF NOT EXISTS `comentario` (
  `id_comentario` int NOT NULL AUTO_INCREMENT,
  `id_producto` int DEFAULT NULL,
  `contenido` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `fecha_comentario` date DEFAULT NULL,
  `id_usuario` int DEFAULT NULL,
  PRIMARY KEY (`id_comentario`),
  KEY `id_producto` (`id_producto`),
  KEY `fk_comentario_usuario` (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `comentario`
--

INSERT INTO `comentario` (`id_comentario`, `id_producto`, `contenido`, `fecha_comentario`, `id_usuario`) VALUES
(2, 1, 'pesa pesada', '2023-05-22', NULL),
(3, 1, 'pesa pesada de 10 kg', '2023-05-22', NULL),
(4, 6, 'La mejor creatina del mercado', '2023-05-22', NULL),
(5, 5, 'Proteina whey de suero de leche', '2023-05-22', NULL),
(6, 1, 'pesa pesada', '2023-05-25', 1),
(7, 5, 'He comprado esta proteina siendo el usuario ale2', '2023-05-25', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `compra`
--

DROP TABLE IF EXISTS `compra`;
CREATE TABLE IF NOT EXISTS `compra` (
  `id_compra` int NOT NULL,
  `fecha_compra` date DEFAULT NULL,
  `precio` decimal(10,0) DEFAULT NULL,
  `id_usuario` int DEFAULT NULL,
  PRIMARY KEY (`id_compra`),
  KEY `fk_compra_usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `compra`
--

INSERT INTO `compra` (`id_compra`, `fecha_compra`, `precio`, `id_usuario`) VALUES
(53664, '2023-05-29', '85', 1),
(79036, '2023-06-02', '225', 1),
(136075, '2023-06-02', '95', 9),
(156629, '2023-05-29', '80', 1),
(518903, '2023-05-29', '200', 1),
(638663, '2023-06-02', '203', 8),
(901010, '2023-05-29', '9', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_compra`
--

DROP TABLE IF EXISTS `detalle_compra`;
CREATE TABLE IF NOT EXISTS `detalle_compra` (
  `id_detalle` int NOT NULL AUTO_INCREMENT,
  `id_compra` int DEFAULT NULL,
  `id_producto` int DEFAULT NULL,
  `cantidad` int DEFAULT NULL,
  PRIMARY KEY (`id_detalle`),
  KEY `id_compra` (`id_compra`),
  KEY `detalle_compra_ibfk_2` (`id_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=119 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `detalle_compra`
--

INSERT INTO `detalle_compra` (`id_detalle`, `id_compra`, `id_producto`, `cantidad`) VALUES
(94, 53664, 5, 1),
(95, 53664, 6, 1),
(96, 53664, 7, 1),
(97, 518903, 1, 1),
(98, 518903, 5, 1),
(99, 518903, 6, 1),
(100, 518903, 7, 1),
(101, 518903, 8, 1),
(102, 901010, 10, 3),
(103, 156629, 9, 1),
(104, 156629, 7, 2),
(105, 156629, 5, 1),
(106, 79036, 7, 1),
(107, 79036, 6, 4),
(108, 79036, 5, 1),
(109, 79036, 1, 2),
(110, 638663, 1, 1),
(111, 638663, 5, 1),
(112, 638663, 6, 1),
(113, 638663, 7, 1),
(114, 638663, 8, 1),
(115, 638663, 10, 1),
(116, 136075, 5, 1),
(117, 136075, 1, 1),
(118, 136075, 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `producto`
--

DROP TABLE IF EXISTS `producto`;
CREATE TABLE IF NOT EXISTS `producto` (
  `id_producto` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `descripcion` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT NULL,
  `cantidad` int NOT NULL,
  `imagen` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id_producto`),
  UNIQUE KEY `imagen` (`imagen`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `producto`
--

INSERT INTO `producto` (`id_producto`, `nombre`, `descripcion`, `precio`, `cantidad`, `imagen`) VALUES
(1, 'Mancuerna', 'mancuernas pesasas', '25.00', 93, '/assets/fotos/mancuerna.jpg'),
(5, 'Proteina', 'Proteina en polvo de suero de leche', '40.00', 93, '/assets/fotos/proteina.jpg'),
(6, 'Creatina', 'Creatina en polvo para tomar', '30.00', 91, '/assets/fotos/creatina.jpg'),
(7, 'Straps', 'Straps para mejorar el agarre a la barra,muy comodos', '15.00', 93, '/assets/fotos/straps.jpg'),
(8, 'Cinturon', 'Cinturon abdominal,valido para soportar pesos,aguantar la fuerza abdominal y mejorar tu postura además de ayudarte con la carga de la espalda', '90.00', 96, '/assets/fotos/cinturon.jpg'),
(9, 'Codera', 'Codera para evitar lesiones y que se flexionen demas los codos', '10.00', 96, '/assets/fotos/codera.jpg'),
(10, 'Barrita proteica', 'Barritas energeticas,para desayunos rapidos de alto contenido proteico', '3.00', 96, '/assets/fotos/barrita.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `contrasena` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id_usuario`, `nombre`, `email`, `contrasena`) VALUES
(1, 'ale2', 'ale@gmail.com', '1234'),
(8, 'admoralh01', 'admoralh01@iesarroyoharnina.es', '123'),
(9, 'juanito', 'juan@gmail.com', '1234');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `comentario`
--
ALTER TABLE `comentario`
  ADD CONSTRAINT `comentario_ibfk_1` FOREIGN KEY (`id_producto`) REFERENCES `producto` (`id_producto`),
  ADD CONSTRAINT `fk_comentario_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE SET NULL;

--
-- Filtros para la tabla `compra`
--
ALTER TABLE `compra`
  ADD CONSTRAINT `fk_compra_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE SET NULL;

--
-- Filtros para la tabla `detalle_compra`
--
ALTER TABLE `detalle_compra`
  ADD CONSTRAINT `detalle_compra_ibfk_1` FOREIGN KEY (`id_compra`) REFERENCES `compra` (`id_compra`),
  ADD CONSTRAINT `detalle_compra_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `producto` (`id_producto`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
