Create database db_aaeea5_sysrest

CREATE TABLE compra (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  numeroFactura VARCHAR(50) NOT NULL DEFAULT '0',
  fecha DATE NOT NULL,
  precioUnitario DECIMAL(20,6) NOT NULL DEFAULT '0',
  subTotal DECIMAL(20,6) NOT NULL DEFAULT '0',
  iva DECIMAL(20,6) NOT NULL DEFAULT '0',
  total DECIMAL(20,6) NOT NULL DEFAULT '0',
  IdProducto INT NOT NULL,
  IdProveedor INT NOT NULL,
  FOREIGN KEY (IdProducto) REFERENCES producto(Id),
  FOREIGN KEY (IdProveedor) REFERENCES proveedor(Id)
);

CREATE TABLE datospersonales (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Apellido VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  telefono VARCHAR(50) NOT NULL,
  IdUsuario INT NOT NULL,
  FOREIGN KEY (IdUsuario) REFERENCES usuarios(id)
);

CREATE TABLE detallefactura (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdFactura INT NOT NULL,
  IdPlatillo INT NOT NULL,
  Cantidad INT NOT NULL,
  PrecioUnitario DECIMAL(10,2) NOT NULL,
  Subtotal DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  FOREIGN KEY (IdFactura) REFERENCES factura(Id),
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id)
);

CREATE TABLE empleado (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Apellido VARCHAR(50) NOT NULL,
  Email VARCHAR(100) NOT NULL UNIQUE,
  Telefono VARCHAR(15) NOT NULL,
  FechaContratacion DATE NOT NULL,
  Puesto VARCHAR(50) NOT NULL,
  Salario DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  Estado TINYINT NOT NULL DEFAULT 1
);

CREATE TABLE factura (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPedido INT NOT NULL,
  FechaEmision DATETIME NOT NULL,
  IdCliente INT NOT NULL,
  Total DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  MetodoDePago VARCHAR(50) NOT NULL,
  NumeroFactura VARCHAR(50) NOT NULL UNIQUE,
  FOREIGN KEY (IdPedido) REFERENCES pedido(id),
  FOREIGN KEY (IdCliente) REFERENCES datospersonales(Id)
);

CREATE TABLE inventario (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL DEFAULT '0',
  Descripcion VARCHAR(250) NOT NULL DEFAULT '0',
  CantidadDisponible INT NOT NULL DEFAULT 0,
  UnidadMedida VARCHAR(250) NOT NULL DEFAULT '0',
  CantidadMinima INT NOT NULL DEFAULT 0,
  CostoUnitario DECIMAL(20,6) NOT NULL DEFAULT 0,
  FechaUltimaCompra DATE NOT NULL,
  FechaCaducidadLote DATE NOT NULL,
  IdCompra INT NOT NULL,
  FOREIGN KEY (IdCompra) REFERENCES compra(Id)
);

CREATE TABLE mesas (
  id INT AUTO_INCREMENT PRIMARY KEY,
  descripcion VARCHAR(250),
  capacidad INT NOT NULL,
  disponibilidad TINYINT NOT NULL
);

CREATE TABLE pedido (
  id INT AUTO_INCREMENT PRIMARY KEY,
  IdCliente INT NOT NULL,
  fecha_hora_pedido DATETIME NOT NULL,
  Estado TINYINT NOT NULL,
  Comentarios VARCHAR(250),
  IdMesa INT NOT NULL,
  Total DECIMAL(20,6) NOT NULL,
  Fecha_hora_entrega DATETIME NOT NULL,
  FOREIGN KEY (IdCliente) REFERENCES datospersonales(Id),
  FOREIGN KEY (IdMesa) REFERENCES mesas(id)
);

CREATE TABLE pedidoplatillo (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPedido INT NOT NULL,
  IdPlatillo INT NOT NULL,
  Cantidad INT NOT NULL,
  PrecioUnitario DECIMAL(10,2) NOT NULL,
  Comentario VARCHAR(255),
  FOREIGN KEY (IdPedido) REFERENCES pedido(id),
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id)
);

CREATE TABLE platillo (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL DEFAULT '0',
  Descripcion VARCHAR(50) NOT NULL DEFAULT '0',
  Precio DECIMAL(20,6) NOT NULL DEFAULT '0',
  Disponibilidad TINYINT NOT NULL DEFAULT 1,
  TiempoPreparacion TIME NOT NULL DEFAULT '00:00:00',
  IngredientePrincipal VARCHAR(50) NOT NULL DEFAULT '0',
  FechaActualizacion DATE NOT NULL,
  IdCategoria INT,
  FOREIGN KEY (IdCategoria) REFERENCES categoria(Id)
);

CREATE TABLE platillo_imagen (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPlatillo INT NOT NULL,
  Imagen LONGBLOB NOT NULL,
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id)
);

CREATE TABLE producto (
  Id INT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL DEFAULT '',
  contenidoEmpaque VARCHAR(50) NOT NULL DEFAULT ''
);

CREATE TABLE proveedor (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL DEFAULT '0',
  Contacto VARCHAR(50) NOT NULL DEFAULT '0',
  Direccion VARCHAR(50) NOT NULL DEFAULT '0'
);

CREATE TABLE reservacion (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdUsuario INT,
  IdMesa INT,
  FechaResvervacion DATETIME DEFAULT NOW(),
  HoraLlegada TIME DEFAULT '00:00:00',
  HoraSalida TIME DEFAULT '00:00:00',
  INDEX (IdUsuario),
  INDEX (IdMesa),
  FOREIGN KEY (IdUsuario) REFERENCES usuarios(id),
  FOREIGN KEY (IdMesa) REFERENCES mesas(id)
);

CREATE TABLE roles (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(150)
);

CREATE TABLE usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50),
  clave VARCHAR(10) NOT NULL,
  IdRoles INT NOT NULL,
  FOREIGN KEY (IdRoles) REFERENCES roles(id)
);

CREATE TABLE categoria (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL DEFAULT '0',
  Descripcion VARCHAR(100)
);

