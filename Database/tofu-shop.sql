create database carshop_db
--use carshop_db

USE MASTER

-- Users table
CREATE TABLE Users (
    userId INT IDENTITY(1,1) NOT NULL,
    [name] VARCHAR(100),
    email VARCHAR(100),
    [address] NVARCHAR(255),
    phone VARCHAR(15),
    username VARCHAR(100) UNIQUE,
    [password] VARCHAR(100),
    role NVARCHAR(20) NOT NULL DEFAULT 'Customer', -- Added role column with default value 'Customer'
    PRIMARY KEY(userId)
);

-- Add a constraint to ensure only valid roles are used
ALTER TABLE Users
ADD CONSTRAINT CK_User_Role CHECK (role IN ('Admin', 'Staff', 'Customer'));

-- Model table
CREATE TABLE Models (
    modelId INT IDENTITY(1,1) not null,
    modelName VARCHAR(100),
	PRIMARY KEY(modelId)
);

--Discount table
CREATE TABLE Discounts (
	discountId INT IDENTITY(1,1) not null,
	discountRate INT,
	PRIMARY KEY(discountId)
);

-- Product table
CREATE TABLE Products (
    productId INT IDENTITY(1,1) not null,
    productName VARCHAR(100),
    price DECIMAL(10, 2),
    [image] VARCHAR(MAX),
	[Description] VARCHAR(MAX),
	PRIMARY KEY(productId),
    modelId INT FOREIGN KEY REFERENCES Models(modelId),
    discountId INT FOREIGN KEY REFERENCES Discounts(discountId),
	quantity INT
);

-- ProductOrder table
CREATE TABLE ProductOrder (
    orderId INT IDENTITY(1,1) not null,
	userId INT,
    [date] DATE DEFAULT GETDATE(),
	amount int,
	PRIMARY KEY(orderId),
	FOREIGN KEY (userId) REFERENCES Users(userId)
);

-- OrderDetail table
CREATE TABLE OrderDetail (
    orderID INT,
    productId INT,
    unitPrice DECIMAL(10, 2),
    quantity INT,
	PRIMARY KEY(orderID, productId),
	FOREIGN KEY (orderID) REFERENCES ProductOrder(orderId),
	FOREIGN KEY (productId) REFERENCES Products(productId)
);

-- Service table
CREATE TABLE UserServices (
	serviceId INT IDENTITY(1,1) not null,
	userId int,
	[message] VARCHAR(MAX),
	[date] DATE DEFAULT GETDATE(),
	PRIMARY KEY(serviceId),
	FOREIGN KEY (userId) REFERENCES Users(userId)
);

INSERT INTO Users ([name], email, [address], phone, username, [password], [role])
VALUES 
	(N'Hoa', 'tofu_shop@gmail.com', null, null, 'tofu', '123', 'Admin'), -- role: Admin
    (N'Nolan', 'nolan@gmail.com', N'123 Đường Hoàng Văn Thụ, Quận 1, TP.HCM', '0999999991', 'user1', '123', 'Customer'), -- role: User
    (N'Mary', 'mary@gmail.com', N'456 Main Street, Anytown, Country', '0999999992', 'user2', '123', 'Staff'), -- role: User
    (N'Timothy', 'tim@gmail.com', N'Quận 2, TP.HCM', '0999999993', 'user3', '123', 'Customer'), -- role: User
    (N'John Smith', 'john@gmail.com', N'789 Elm St, Village, Country', '0999999994', 'user4', '123', 'Customer'), -- role: User
    (N'Robert', 'robert@gmail.com', N'147 Quận 3, TP.HCM', '0999999599', 'user5', '123', 'Customer'), -- role: User
    (N'Alice', 'alice@gmail.com', N'246 Oak St, Town, Country', '0996999999', 'user6', '123', 'Staff'), -- role: User
    (N'Henry', 'Henry@gmail.com', N'369 Đường Trần Hưng Đạo, Quận 4, TP.HCM', '0999979999', 'user7', '123', 'Staff'); -- role: User
    

INSERT INTO Models (modelName)
VALUES 
    (N'Sport'),
    (N'SUV'),
    (N'Sedan'),
	(N'Coupes'),
	(N'Truck')


INSERT INTO Discounts (discountRate)
VALUES 
	(10),
    (15),
    (20),
    (25),
	(30)

-- Insert Products
INSERT INTO Products (productName, price, [image], [Description], modelId, discountId, quantity)
VALUES 
    (N'Bugatti Chiron', 176, 'https://i.imgur.com/mlTNCv3.jpeg'
	, 'A toy car Bugatti Chiron is a scaled-down version of the record-breaking supercar produced by the esteemed French automaker. It is designed to provide children and car enthusiasts with a fun and safe way to experience the thrill of owning and driving a Bugatti Chiron. The toy car is made of high-quality materials, such as die-cast metal or durable plastic, and features intricate details that accurately replicate the design and styling of the real car. It may come in various scales, such as 1:18 or 1:43, and some models may even include working features like opening doors, rolling wheels, and detailed interiors. The Bugatti Chiron toy car is a fantastic way to introduce children to the world of high-performance cars and inspire their imagination. It is also a collectible item for car enthusiasts who want to add a miniature version of this legendary supercar to their collection. With its iconic design, impressive speed, and unmatched luxury, the Bugatti Chiron toy car is a must-have for any toy car collection.'
	, 1 , null, 30),
    (N'Bentley Continental GT', 75, 'https://i.imgur.com/4xIM5ko.jpeg'
	, 'A Bentley Continental GT toy car is a miniature replica of the high-performance sports car produced by the German automaker. It is designed for children and car enthusiasts who want to experience the thrill of driving a Mercedes-Benz AMG GT R in a fun and safe way. The toy car is typically made of durable plastic or metal materials and features a detailed exterior design that replicates the sleek and aggressive look of the real car. It may come in various scales, such as 1:24 or 1:43, and some models may even include working features like opening doors, rolling wheels, and detailed interiors. The Mercedes-Benz AMG GT R toy car is a great way for children to learn about cars, develop their motor skills, and indulge in their imagination. It is also a collectible item for car enthusiasts who want to add a miniature version of this impressive sports car to their collection.'
	, 1 , null, 30),
    (N'Ford Mustang Shelby GT350R', 105, 'https://i.imgur.com/AQ4x7MF.jpeg'
	, 'The toy car is made of high-quality materials, such as die-cast metal or durable plastic, and features intricate details that accurately replicate the design and styling of the real car. It may come in various scales, such as 1:18 or 1:43, and some models may even include working features like opening doors, rolling wheels, and detailed interiors. The Ford Mustang Shelby GT350R toy car is a fantastic way to introduce children to the world of high-performance cars and inspire their imagination. It is also a collectible item for car enthusiasts who want to add a miniature version of this legendary muscle car to their collection. With its impressive speed, handling, and racing pedigree, the Ford Mustang Shelby GT350R toy car is a must-have for any toy car collection.'
	, 1 , null, 30),
	(N'Aston Martin DBS', 32, 'https://i.imgur.com/YKGEQlT.jpeg'
	, 'Aston Martin DBS sports car toy, where elegance meets exhilaration in miniature form! Inspired by the iconic Aston Martin brand, this scaled-down replica captures the essence of luxury and high-performance driving. With its sleek and aerodynamic design, this toy car exudes sophistication from every angle. From the distinctive Aston Martin grille to the sleek lines of the body, every detail reflects the craftsmanship and attention to detail synonymous with the Aston Martin name. Step inside the finely detailed interior to experience the thrill of driving a true sports car. Built for speed and agility, the Aston Martin DBS toy car features smooth-rolling wheels and precise handling, making it perfect for high-speed adventures on any pretend racetrack.'
	, 1 , null, 30),
	(N'Chevrolet Corvette Stingray', 225, 'https://i.imgur.com/lTB3QOe.jpeg'
	, 'Chevrolet Corvette Stingray toy car, a miniature marvel that embodies the spirit of American muscle and performance! Crafted with meticulous attention to detail, this scaled-down replica captures the iconic design and powerful presence of the Corvette Stingray. With its sleek lines and aerodynamic curves, this toy car commands attention on any imaginary road. From the distinctive Stingray grille to the bold rear spoiler, every feature reflects the Corvette\''s legacy of speed and style.Perfect for young drivers and Corvette enthusiasts alike, this toy car invites children to unleash their imagination and embark on thrilling adventures.'
	, 1, 2, 30),
	(N'Land Rover Defender', 55, 'https://i.imgur.com/Zt6zsl1.jpeg'
	, 'Introducing the rugged and iconic Land Rover Defender toy car! Modeled after the legendary off-road vehicle, this miniature version embodies the spirit of adventure and exploration. Featuring authentic details and a durable design, this toy car is built to withstand the toughest playtime adventures. With its signature boxy shape and rugged tires, it\''s ready to conquer any terrain, whether it\''s in the backyard or the living room floor. Perfect for young adventurers and Land Rover enthusiasts alike, this Defender toy car sparks imagination and encourages endless hours of creative play. Whether navigating imaginary trails or embarking on epic expeditions, the Land Rover Defender toy car is sure to inspire thrilling adventures for kids of all ages. Bring the excitement of off-road exploration home with the Land Rover Defender toy car and let the adventures begin!'
	, 2,  null, 30),
	(N'Lexus GX550', 168, 'https://i.imgur.com/iJ6MlLl.jpeg'
	, 'The Lexus GX550 SUV toy car, where adventure meets luxury in miniature form! Crafted with meticulous attention to detail, this scaled-down replica captures the essence of sophistication and performance synonymous with the Lexus brand. From its sleek and aerodynamic design to its iconic spindle grille, every feature mirrors the elegance of the full-size GX550. Step inside the intricately designed interior and discover a world of luxury, with finely crafted seats and dashboard details that elevate playtime to a new level of refinement.'
	, 2, 4, 30),
	(N'Hyundai Tucson', 28, 'https://i.imgur.com/CGzF1e2.jpeg'
	, 'The innovative and eco-friendly Rivian R1S SUV toy car! Inspired by the groundbreaking electric vehicle, this miniature replica brings cutting-edge technology and sustainable design to the world of toy cars. With its sleek lines and modern styling, this toy car mirrors the distinctive look of the Rivian R1S, capturing attention and imagination alike. Crafted with attention to detail and built to last, it promises hours of exciting playtime adventures. Perfect for eco-conscious kids and tech-savvy enthusiasts, the Rivian R1S toy car offers a glimpse into the future of automotive innovation. Let imaginations soar as young drivers embark on eco-friendly journeys, exploring new horizons and saving the planet one imaginary mile at a time.' 
	, 2,  null, 30),
	(N'Black Rolls Royce Cullinan', 325, 'https://i.imgur.com/1QCMbl9.jpeg'
	, 'The Black Rolls Royce Cullinan toy car. Inspired by the legendary luxury SUV, this scaled-down replica captures the essence of sophistication and style in a pint-sized package. Crafted with meticulous attention to detail, this toy car features a sleek black finish and iconic Rolls Royce design elements, making it a standout addition to any collection. Perfect for enthusiasts of all ages, the Black Rolls Royce Cullinan toy car offers endless hours of imaginative play and display. Whether adorning a shelf or zooming around a make-believe track, this miniature masterpiece is sure to delight and impress. Experience the luxury of the Rolls Royce Cullinan in miniature form and add a touch of elegance to your collection with this exquisite toy car.' 
	, 2, 1, 30),
	(N'Volvo XC40', 325, 'https://i.imgur.com/Otx0t7P.jpeg'
	, 'Volvo XC40 SUV toy car, where safety meets style in miniature form! Crafted with meticulous attention to detail, this scaled-down replica captures the sleek and sophisticated design of the Volvo XC40. With its iconic Volvo grille and distinctive body lines, this toy car exudes elegance and modernity. Step inside the intricately crafted interior to discover a world of luxury, with finely detailed seats and dashboard elements that elevate playtime to a new level of sophistication. Perfect for young drivers and Volvo enthusiasts alike, the XC40 SUV toy car invites children to experience the joy of driving with a Volvo, where safety and style go hand in hand. Join the Volvo family and embark on unforgettable adventures with the XC40 toy car!' 
	, 2,  null, 30),
	(N'Audi A8', 83, 'https://i.imgur.com/BM1Jyol.jpeg'
	, 'The Audi A8  – a sleek and sophisticated miniature replica of the prestigious luxury sedan. With its exquisite attention to detail and premium craftsmanship, this scaled-down version captures the essence of Audi\''s flagship model. Crafted for discerning young drivers and collectors alike, the Audi A8 toy car boasts stunning design elements and realistic features that mimic the elegance of the real thing. From its iconic grille to its sleek contours, every aspect reflects the luxury and innovation synonymous with the Audi brand. Built to withstand the rigors of playtime, yet detailed enough for display, this toy car is perfect for both imaginative adventures and showcasing on shelves. Elevate playtime to new levels of sophistication with the Audi A8 toy car, available now at our store.'
	, 3, null, 30),
	(N'BMW 740Li', 96, 'https://i.imgur.com/KA97cbx.jpeg'
	, 'The BMW 740Li – a captivating miniature rendition of the legendary luxury sedan. Meticulously crafted to mirror the elegance and prestige of its full-sized counterpart, this scaled-down model exudes sophistication and style. Featuring intricate detailing and sleek lines, the BMW 740Li captures the essence of BMW\''s renowned craftsmanship and engineering excellence. From its distinctive kidney grille to its sleek profile, every aspect reflects the hallmark design cues that define the BMW brand. Built to withstand the demands of young drivers and collectors alike, this toy car combines durability with authenticity, making it perfect for both playtime adventures and display purposes. Elevate your child\''s imagination and add a touch of automotive luxury to their collection with the BMW 740Li, available now at our store.' 
	, 3, null, 30),
	(N'Cadillac CT5', 44, 'https://i.imgur.com/BzYB5DE.jpeg'
	, 'The Cadillac CT5 toy car in a classic 1:32 scale – a captivating miniature rendition of Cadillac\''s distinguished luxury sedan. Crafted with meticulous attention to detail, this scaled-down model captures the essence of the CT5\''s sophisticated design and unparalleled elegance. From its iconic grille to its sleek contours, every aspect of this miniature masterpiece reflects Cadillac\''s legacy of excellence and innovation. Whether displayed proudly on a shelf or racing through imaginary streets, the CT5 toy car is sure to ignite the imagination of collectors and enthusiasts alike. Built to withstand the rigors of playtime while maintaining authenticity, this 1:32 scale toy car promises hours of enjoyment and admiration. Elevate your collection with the Cadillac CT5 toy car, available now at our store.' 
	, 3, 3, 30),
	(N'Nissan Altima', 35, 'https://i.imgur.com/kd9Ueix.jpeg'
	, 'The Nissan Altima toy car in a classic 1:32 scale – a stunning replica that captures the essence of Nissan\''s beloved sedan. Precision-crafted with meticulous attention to detail, this scaled-down model mirrors the sleek lines and dynamic design of the full-sized Altima. Featuring a striking exterior with authentic branding and intricate detailing, this miniature masterpiece brings the thrill of the open road right to your fingertips. Whether it\''s for imaginative play or display, the Nissan Altima toy car is built to impress. Perfect for collectors and young enthusiasts alike, this 1:32 scale toy car promises hours of enjoyment and admiration. Add a touch of automotive excellence to your collection with the Nissan Altima toy car, available now at our store.' 
	, 3, null, 30),
	(N'Rolls Royce Phantom', 35, 'https://i.imgur.com/2gVyn8J.jpeg'
	, 'Discover the Rolls Royce Phantom toy car, a mini marvel that brings the essence of the iconic sedan to your child\''s playtime. Crafted with meticulous attention to detail, this scaled-down replica captures the sleek lines and dynamic design of its full-sized counterpart. Perfect for little drivers with big imaginations, the Camry Toyota toy car promises endless hours of interactive fun. Made from durable materials, it\''s built to withstand the demands of energetic play while igniting creativity and sparking adventurous journeys. Whether zooming through imaginary city streets or embarking on make-believe road trips, the Camry Toyota toy car is sure to delight young automotive enthusiasts. Add a touch of automotive excellence to playtime with this captivating miniature masterpiece, available now at our store.' 
	, 3, null, 30),
	(N'Lexus LC500', 653, 'https://i.imgur.com/MWgyQhw.jpeg'
	, 'The Lexus LC500 toy car, downsized to a finely detailed 1:32 scale – a miniature marvel that captures the essence of Ferrari\''s cutting-edge design and high-performance engineering. Expertly crafted with meticulous attention to detail, this scaled replica embodies the sleek lines and dynamic spirit of the iconic 296 GTB. From its distinctive Ferrari grille to its aerodynamic contours, every feature is faithfully reproduced to showcase the car\''s legendary style and speed. Whether it\''s zooming around imaginary racetracks or proudly displayed as a collector\''s item, this miniature masterpiece promises to ignite the imagination and inspire endless admiration. Built to withstand the excitement of playtime while preserving its authentic allure, the Ferrari 296 GTB toy car in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Ferrari 296 GTB toy car, available now at our store.' 
	, 4, null, 30),
	(N'BMW Series 4', 384, 'https://i.imgur.com/qz9twjW.jpeg'
	, 'The BMW Series 4 toy car, expertly crafted in a meticulously detailed 1:32 scale – a miniature masterpiece that captures the essence of Lotus legendary sports car. With precision engineering and exquisite attention to detail, this scaled replica embodies the sleek lines and dynamic performance of the Emira. From its iconic Lotus badge to its aerodynamic contours, every feature is faithfully recreated to showcase the car\''s distinctive style and agility. Whether it\''s speeding around imaginary racetracks or proudly displayed as a collector\''s item, this miniature marvel promises to ignite the imagination and inspire endless admiration. Built to withstand the excitement of playtime while preserving its authentic allure, the Lotus Emira toy car in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Lotus Emira toy car, available now at our store.' 
	, 4, null, 30),
	(N'Maserati GranTurismo', 193, 'https://i.imgur.com/iE4pRXT.jpeg'
	, 'The Maserati GranTurismo toy car, meticulously crafted in a finely detailed 1:32 scale – a miniature masterpiece that captures the elegance and performance of Maserati\''s iconic grand tourer. With precision engineering and exquisite attention to detail, this scaled replica embodies the sleek lines and dynamic spirit of the GranTurismo. From its iconic Maserati emblem to its aerodynamic silhouette, every feature is faithfully recreated to showcase the car\''s timeless style and exhilarating presence. Whether it\''s cruising through imaginary streets or displayed proudly as a collector\''s item, this miniature marvel promises to inspire endless admiration and excitement. Built to endure the thrills of playtime while preserving its authentic allure, the Maserati GranTurismo toy car in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Maserati GranTurismo toy car, available now at our store.' 
	, 4, null, 30),
	(N'Porsche Cayman', 89, 'https://i.imgur.com/y5V5MOD.jpeg'
	, 'The Porsche 718 Cayman toy car, meticulously crafted in a detailed 1:32 scale – a miniature marvel that encapsulates the iconic design and exhilarating performance of Porsche\''s legendary sports car. Precision-engineered with exquisite attention to detail, this scaled replica embodies the sleek lines and dynamic spirit of the 718 Cayman. From its iconic Porsche badge to its aerodynamic curves, every feature is faithfully recreated to showcase the car\''s renowned style and agility. Whether it\''s tearing up imaginary racetracks or proudly displayed as a collector\''s piece, this miniature masterpiece promises to spark endless excitement and admiration. Built to withstand the thrills of playtime while preserving its authentic allure, the Porsche 718 Cayman toy car in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Porsche 718 Cayman toy car, available now at our store.' 
	, 4, null, 30),
	(N'Subaru SBZ', 111, 'https://i.imgur.com/uuZP3rI.jpeg'
	, 'The Subaru SBZ toy car, expertly crafted in a finely detailed 1:32 scale – a miniature marvel that embodies the adventurous spirit and performance of Subaru\''s iconic vehicles. With precision engineering and meticulous attention to detail, this scaled replica captures the essence of the SBZ\''s rugged design and dynamic capabilities. From its distinctive Subaru emblem to its rugged exterior features, every aspect is faithfully recreated to showcase the car\''s bold style and versatility. Whether it\''s conquering imaginary off-road trails or displayed proudly as a collector\''s piece, this miniature masterpiece promises to spark endless excitement and admiration. Built to withstand the rigors of playtime while preserving its authentic allure, the Subaru SBZ toy car in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Subaru SBZ toy car, available now at our store.' 
	, 4, 5, 30),
	(N'Ford Ranger', 268, 'https://i.imgur.com/9MON595.jpeg'
	, 'The Ford Ranger toy truck in a meticulously crafted 1:32 scale – a miniature powerhouse that embodies the spirit of toughness and versatility. Designed with precision and attention to detail, this scaled-down replica captures the essence of Ford\''s legendary pickup. From its bold front grille to its rugged exterior accents, every feature of the Ranger truck mirrors the durability and capability of its real-life counterpart. Whether it\''s conquering imaginary off-road trails or proudly displayed on a collector\''s shelf, this miniature masterpiece promises to inspire endless adventures. Ideal for both young enthusiasts and seasoned collectors, the Ford Ranger toy truck in 1:32 scale is built to endure the rigors of playtime while maintaining its authentic charm. Elevate your collection with the Ford Ranger toy truck, available now at our store.' 
	, 5, null, 30),
	(N'GMC Sierra 1500', 173, 'https://i.imgur.com/ezHkDeW.jpeg'
	, 'the GMC Sierra 1500 toy truck in a meticulously crafted 1:32 scale – a miniature marvel that embodies the rugged power and iconic style of GMC\''s renowned pickup. Expertly detailed and designed, this scaled-down replica captures the essence of the Sierra 1500\''s robust presence and unmatched versatility. From its imposing front grille to its rugged bed, every aspect of the Sierra 1500 toy truck mirrors the boldness and functionality of its full-sized counterpart. Whether it\''s tackling imaginary off-road adventures or proudly displayed on a collector\''s shelf, this miniature masterpiece promises to ignite the imagination and inspire endless journeys. Perfect for enthusiasts of all ages, the GMC Sierra 1500 toy truck in 1:32 scale is built to endure the excitement of playtime while maintaining its authentic charm. Elevate your collection with the GMC Sierra 1500 toy truck, available now at our store.' 
	, 5, null, 30),
	(N'Honda Ridgeline', 69, 'https://i.imgur.com/JNTwmGv.jpeg'
	, 'The Honda Ridgeline toy truck, downsized to a detailed 1:32 scale – a miniature powerhouse echoing the bold spirit of Honda\''s iconic pickup. Crafted with precision and flair, this scaled model captures the rugged charm and modern design of the Ridgeline. From its iconic front grille to its sturdy bed, every feature is meticulously replicated to showcase the Ridgeline\''s distinctive style and functionality. Whether it\''s conquering make-believe off-road trails or adorning your shelf as a collectible, this miniature masterpiece promises to spark endless imaginative adventures. Built to withstand the rigors of playtime while preserving its authentic allure, the Honda Ridgeline toy truck in 1:32 scale is a must-have for enthusiasts of all ages. Elevate your collection with the Honda Ridgeline toy truck, available now at our store.'
	, 5, 3, 30),
	(N'Huyndai Santa Cruz', 271, 'https://i.imgur.com/ar7ItqK.jpeg'
	, 'The Hyundai Santa Cruz toy truck in a finely detailed 1:32 scale – a captivating miniature rendition of Hyundai\''s innovative pickup. Crafted with precision and authenticity, this scaled-down replica captures the essence of the Santa Cruz\''s modern design and adventurous spirit. From its distinctive front grille to its sleek body contours, every detail of the Santa Cruz truck mirrors the style and versatility of its full-sized counterpart. Whether it\''s embarking on imaginary road trips or proudly displayed on a collector\''s shelf, this miniature masterpiece promises to ignite the imagination and inspire endless journeys. Perfect for enthusiasts of all ages, the Hyundai Santa Cruz toy truck in 1:32 scale is built to withstand the demands of playtime while maintaining its authentic appeal. Elevate your collection with the Hyundai Santa Cruz toy truck, available now at our store.' 
	, 5, null, 30),
	(N'Jeep Gladiator', 53, 'https://i.imgur.com/YpV5ZWD.jpeg'
	, 'The Jeep Gladiator toy truck in a ruggedly detailed 1:32 scale – a miniature marvel that embodies the spirit of adventure and off-road capability. Crafted with precision and authenticity, this scaled-down replica captures the essence of Jeep\''s legendary pickup. From its iconic seven-slot grille to its rugged exterior features, every detail of the Gladiator truck reflects the ruggedness and durability of its real-life counterpart. Whether navigating imaginary trails or proudly displayed on a collector\''s shelf, this miniature masterpiece promises to evoke the thrill of outdoor exploration. Perfect for young adventurers and collectors alike, the Jeep Gladiator toy truck in 1:32 scale is built to withstand the demands of playtime while maintaining its authenticity. Add a touch of rugged excitement to your collection with the Jeep Gladiator toy truck, available now at our store.' 
	, 5, null, 30),
	(N'Ford Mustang', 55000, 'https://i.imgur.com/2Y9O8Cj.jpeg', 
'A classic American muscle car', 1, NULL, 8),
('Chevrolet Camaro', 52000, 'https://i.imgur.com/QFnqULE.png', 
'A high-performance sports car', 1, NULL, 7),
('Porsche 911', 120000, 'https://i.imgur.com/yz7cmQJ.jpeg', 
'A luxury sports car with iconic design', 1, NULL, 3),
('Ferrari 488', 250000, 'https://i.imgur.com/IIT53H6.jpeg', 
'An exotic sports car with incredible speed', 1, NULL, 2),
('Lamborghini Huracan', 280000, 'https://i.imgur.com/xGhVNsu.jpeg', 
'A high-performance supercar', 1, NULL, 1),
('Tesla Model S', 80000, 'https://i.imgur.com/bqJj8m8.jpeg', 
'A luxury electric sedan with advanced features', 2, NULL, 5),
('BMW X3', 43000, 'https://i.imgur.com/IuMiRzc.jpeg', 
'A compact luxury SUV with sporty handling', 2, NULL, 6),
('Audi Q7', 65000, 'https://i.imgur.com/deOy2mB.jpeg', 
'A midsize luxury SUV with three rows of seating', 2, NULL, 4),
('Mercedes-Benz GLC', 54000, 'https://i.imgur.com/R9jRm58.jpeg', 
'A luxury compact SUV with a premium interior', 2, NULL, 6),
('Volvo XC60', 45000, 'https://i.imgur.com/7RkMGiR.jpeg', 
'A safe and stylish compact luxury SUV', 2, NULL, 7),
('Honda Accord', 28000, 'https://i.imgur.com/xVF5HfD.jpeg', 
'A roomy and well-equipped midsize sedan', 3, NULL, 16),
('Toyota Camry', 29000, 'https://i.imgur.com/ltwuutH.jpeg', 
'A reliable and fuel-efficient midsize sedan', 3, NULL, 14),
('Nissan Maxima', 33000, 'https://i.imgur.com/WONbzYV.jpeg', 
'A sporty and upscale full-size sedan', 3, NULL, 9),
('Hyundai Sonata', 24000, 'https://i.imgur.com/iKx0HzL.png', 
'A stylish and feature-rich midsize sedan', 3, NULL, 12),
('Kia Optima', 23000, 'https://i.imgur.com/ErtvV6t.jpeg', 
'A comfortable and efficient midsize sedan', 3, NULL, 11),
('Chevrolet Corvette', 60000, 'https://i.imgur.com/MSGBH3t.jpeg', 
'A high-performance sports car', 4, NULL, 4),
('Jaguar F-Type', 70000, 'https://i.imgur.com/fyT4qXf.jpeg', 
'A luxury sports coupe with dynamic performance', 4, NULL, 3),
('BMW 4 Series', 48000, 'https://i.imgur.com/5WVySD5.jpeg', 
'A luxury coupe with sporty handling', 4, NULL, 5),
('Audi A5', 47000, 'https://i.imgur.com/H75uo4W.jpeg', 
'A stylish and high-tech luxury coupe', 4, NULL, 6),
('Mercedes-Benz E-Class Coupe', 65000, 'https://i.imgur.com/wyi3oiR.jpeg', 
'A luxury coupe with a smooth ride', 4, NULL, 4),
('Ford F-150', 30000, 'https://i.imgur.com/RmVrwspb.jpg', 
'A powerful and durable pickup truck', 5, NULL, 5),
('Chevrolet Silverado', 32000, 'https://i.imgur.com/rRrP2bI.jpeg', 
'A strong and dependable full-size pickup', 5, NULL, 8),
('Ram 1500', 35000, 'https://i.imgur.com/vBb8bHFb.jpg', 
'A powerful and comfortable pickup truck', 5, NULL, 7),
('GMC Sierra', 34000, 'https://i.imgur.com/9gs7uLY.jpeg', 
'A robust and reliable full-size pickup', 5, NULL, 8),
('Toyota Tacoma', 31000, 'https://i.imgur.com/W7vg5Qp.jpeg', 
'A versatile and durable midsize pickup truck', 5, NULL, 10),
('Tesla Model 3', 48000, 'https://i.imgur.com/kzcdKQz.jpeg', 
'A high-tech electric sedan', 3, NULL, 5),
('Honda Civic', 22000, 'https://i.imgur.com/FUN8mxN.jpeg', 
'A sporty and comfortable compact car', 3, NULL, 15),
('Mazda 6', 27000, 'https://i.imgur.com/LunVDUv.jpeg', 
'A midsize sedan with sporty handling', 3, NULL, 11),
('Volkswagen Passat', 25000, 'https://i.imgur.com/7lbclYM.png', 
'A spacious and comfortable midsize sedan', 3, NULL, 14),
('Subaru Impreza', 23000, 'https://i.imgur.com/mMAHkDg.jpeg', 
'A compact car with all-wheel drive', 3, NULL, 16),
('BMW X5', 61000, 'https://i.imgur.com/5GYIzfF.jpeg', 
'A luxury midsize SUV with strong performance', 2, NULL, 5),
('Mercedes-Benz GLE', 63000, 'https://i.imgur.com/WhkEFy0.jpeg', 
'A premium midsize SUV with advanced features', 2, NULL, 4),
('Audi Q5', 53000, 'https://i.imgur.com/EAAqzeY.jpeg', 
'A stylish and high-tech compact luxury SUV', 2, NULL, 6),
('Lexus RX', 45000, 'https://i.imgur.com/wpD1qkY.jpeg', 
'A luxury midsize SUV with a smooth ride', 2, NULL, 7),
('Infiniti QX60', 49000, 'https://i.imgur.com/4WYnaca.jpeg', 
'A luxury midsize SUV with three rows', 2, NULL, 9),
('Acura MDX', 47000, 'https://i.imgur.com/9hvyfLH.jpeg', 
'A luxury midsize SUV with sporty handling', 2, NULL, 11),
('Volvo XC90', 60000, 'https://i.imgur.com/VlxbHj6.jpeg', 
'A luxury midsize SUV with advanced safety features', 2, NULL, 8),
('Porsche Cayenne', 80000, 'https://i.imgur.com/ljD7CnCb.jpg', 
'A high-performance luxury midsize SUV', 2, NULL, 3),
('Land Rover Discovery', 65000, 'https://i.imgur.com/qyZjuIMb.jpg', 
'A luxury midsize SUV with off-road capability', 2, NULL, 5),
('Jaguar F-Pace', 70000, 'https://i.imgur.com/WqGVpWTb.jpg', 
'A luxury compact SUV with sporty handling', 2, NULL, 4),
('Alfa Romeo Stelvio', 54000, 'https://i.imgur.com/KvIgiozb.jpg', 
'A luxury compact SUV with Italian style', 2, NULL, 6),
('Tesla Model X', 90000, 'https://i.imgur.com/8pW9wPAb.jpg', 
'A high-tech electric SUV with falcon-wing doors', 2, NULL, 2),
('Cadillac Escalade', 95000, 'https://i.imgur.com/lhiqeIob.jpg', 
'A large luxury SUV with a powerful engine', 2, NULL, 3),
('Lincoln Navigator', 88000, 'https://i.imgur.com/tcaIGmZb.jpg', 
'A large luxury SUV with a refined interior', 2, NULL, 4),
('Bentley Bentayga', 200000, 'https://i.imgur.com/2oYGAsJb.jpg', 
'A super-luxury SUV with exceptional craftsmanship', 2, NULL, 1),
('Rolls-Royce Cullinan', 330000, 'https://i.imgur.com/BzGQ5a5b.jpg', 
'A super-luxury SUV with unparalleled luxury', 2, NULL, 1);

-- Insert into Orders table
INSERT INTO ProductOrder (userId, [date], amount)
VALUES 
    (2, '2024/02/24', 3),
    (3, '2024/02/25', 1),
	(5, '2024/02/25', 2);

-- Insert into ProductOrder table
INSERT INTO OrderDetail (orderID, productId, unitPrice, quantity)
VALUES 
    (1, 1, 86, 2), 
    (1, 8, 28, 1),
	(2, 16, 653, 1),
	(3, 12, 96, 1),
	(3, 24, 71, 1);

--Insert into UserServices table
INSERT INTO UserServices (userId, [message])
VALUES
	(2, 'Good Service !!'),
	(3, 'Great quality products. Love it!'),
	(5, 'Beautiful car and design.');

