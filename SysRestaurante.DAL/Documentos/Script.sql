Create database db_aaeea5_sysrest

CREATE TABLE datosPersonales (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Apellido VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL UNIQUE,
  telefono VARCHAR(15) NOT NULL
);

-- Roles de usuario
CREATE TABLE roles (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(150)
);

-- Usuarios vinculados a roles y persona
CREATE TABLE usuario (
  id INT AUTO_INCREMENT PRIMARY KEY,
   IdDatosPersonales INT NOT NULL,
  clave VARCHAR(10) NOT NULL,
  IdRol INT NOT NULL,
  FOREIGN KEY (IdRol) REFERENCES roles(id),
  FOREIGN KEY (IdDatosPersonales) REFERENCES datosPersonales(Id)
);


CREATE TABLE empleado (
  Id INT PRIMARY KEY,
  FechaContratacion DATE NOT NULL,
  Puesto VARCHAR(50) NOT NULL,
  Salario DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  Estado TINYINT NOT NULL DEFAULT 1,
  FOREIGN KEY (Id) REFERENCES datosPersonales(Id)
);

CREATE TABLE cliente (
  Id INT PRIMARY KEY,
  FOREIGN KEY (Id) REFERENCES datosPersonales(Id)
);


CREATE TABLE categoriaPlatillos (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Descripcion VARCHAR(100)
);

CREATE TABLE categoriaProductos (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Descripcion VARCHAR(100)
);

CREATE TABLE proveedor (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Contacto VARCHAR(50) NOT NULL,
  Direccion VARCHAR(100) NOT NULL
);


CREATE TABLE producto (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  contenidoEmpaque VARCHAR(50) NOT NULL,
  IdCategoriaProducto INT,
  FOREIGN KEY (IdCategoriaProducto) REFERENCES categoriaProductos(Id)
);

CREATE TABLE inventario (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdProducto INT NOT NULL,
  CantidadDisponible INT NOT NULL DEFAULT 0,
  CantidadMinima INT NOT NULL DEFAULT 0,
  CostoUnitario DECIMAL(20,6) NOT NULL DEFAULT 0,
  FechaUltimaCompra DATE NOT NULL,
  FechaCaducidadLote DATE NOT NULL,
  FOREIGN KEY (IdProducto) REFERENCES producto(Id)
);


CREATE TABLE compra (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  numeroFactura VARCHAR(50) NOT NULL,
  fecha DATE NOT NULL,
  iva DECIMAL(20,6) NOT NULL DEFAULT '0',
  total  DECIMAL(8,2) NOT NULL,
  IdProveedor INT NOT NULL,
 FOREIGN KEY (IdProveedor) REFERENCES proveedor(Id) 
);

CREATE TABLE detalleCompra (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdCompra INT NOT NULL,
  IdProducto INT NOT NULL, 
  precioUnitario DECIMAL(20,6) NOT NULL,
  Cantidad INT NOT NULL,
  subTotal DECIMAL(20,6) NOT NULL,
  FOREIGN KEY (IdCompra) REFERENCES compra(Id),
  FOREIGN KEY (IdProducto) REFERENCES producto(Id)
);

-- Mesas del restaurante
CREATE TABLE mesas (
  id INT AUTO_INCREMENT PRIMARY KEY,
  descripcion VARCHAR(250),
  capacidad INT NOT NULL,
  disponibilidad TINYINT NOT NULL
);

-- Pedidos vinculados a clientes y mesas
CREATE TABLE pedido (
  id INT AUTO_INCREMENT PRIMARY KEY,
  IdCliente INT NOT NULL,
  fecha_hora_pedido DATETIME NOT NULL,
  Estado TINYINT NOT NULL,
  Comentarios VARCHAR(250),
  IdMesa INT NOT NULL,
  Fecha_hora_entrega DATETIME NOT NULL,
  Total DECIMAL (8,2) NOT NULL,
  FOREIGN KEY (IdCliente) REFERENCES cliente(Id),
  FOREIGN KEY (IdMesa) REFERENCES mesas(id)
);


CREATE TABLE platillo (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL,
  Descripcion VARCHAR(50),
  Precio DECIMAL(20,6) NOT NULL,
  Disponibilidad TINYINT NOT NULL DEFAULT 1,
  TiempoPreparacion TIME NOT NULL DEFAULT '00:00:00',
  IngredientePrincipal VARCHAR(50),
  FechaActualizacion DATE NOT NULL,
  IdCategoria INT,
  FOREIGN KEY (IdCategoria) REFERENCES categoriaPlatillos(Id)
);

-- Detalle del pedido de cada platillo
CREATE TABLE pedidoPlatillo (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPedido INT NOT NULL,
  IdPlatillo INT NOT NULL,
  Cantidad INT NOT NULL,
  PrecioUnitario DECIMAL(10,2) NOT NULL,
  Comentario VARCHAR(255),
  FOREIGN KEY (IdPedido) REFERENCES pedido(id),
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id)
);

CREATE TABLE platillo_producto (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPlatillo INT NOT NULL,
  IdProducto INT NOT NULL,
  CantidadUsada DECIMAL(10,2) NOT NULL,  -- Cantidad de producto utilizada para este platillo
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id),
  FOREIGN KEY (IdProducto) REFERENCES producto(Id)
);


-- Factura con detalle de cada platillo
CREATE TABLE factura (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdPedido INT NOT NULL,
  FechaEmision DATETIME NOT NULL,
  Total DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  MetodoDePago VARCHAR(50) NOT NULL,
  NumeroFactura VARCHAR(50) NOT NULL UNIQUE,
  FOREIGN KEY (IdPedido) REFERENCES pedido(id)
);

CREATE TABLE detalleFactura (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  IdFactura INT NOT NULL,
  IdPlatillo INT NOT NULL,
  Cantidad INT NOT NULL,
  PrecioUnitario DECIMAL(10,2) NOT NULL,
  Subtotal DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (IdFactura) REFERENCES factura(Id),
  FOREIGN KEY (IdPlatillo) REFERENCES platillo(Id)
);

