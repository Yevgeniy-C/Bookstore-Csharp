--Clear
/*
truncate table Author;
truncate table ProductSerie;
truncate table Category;
truncate table Product;
truncate table ProductDetail;
truncate table ProductAuthor;
*/


---------------
-- Author
---------------
insert into Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	select 'Михаил', 'Евгеньевич', 'Фленов', 'Флёнов Михаил, профессиональный программист. Работал в журнале «Хакер», в котором несколько лет вел рубрики «Hack-FAQ» и «Кодинг» для программистов, печатался в журналах «Игромания» и «Chip-Россия». Автор бестселлеров «Библия Delphi», «Программирование в Delphi глазами хакера», «Программирование на C++ глазами хакера», «Компьютер глазами хакера» и др. Некоторые книги переведены на иностранные языки и изданы в США, Канаде, Польше и других странах.',
		'https://bhv.ru/wp-content/uploads/2021/03/Flenov-Mihail-150x150.jpg',
		'mikhail-flenov'
	where not exists (select from Author where FirstName = 'Михаил' and LastName = 'Фленов');

insert into Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	select 'Евгений', 'Дмитриевич', 'Умрихин', 'Умрихин Евгений Дмитриевич, кандидат технических наук, инженер-программист в крупной региональной страховой компании. Имеет многолетний опыт разработки и внедрения распределенных IT-решений с использованием веб-сервисов ASP.NET и мобильных платформ Android и iOS. Обладатель ряда авторских свидетельств об официальной регистрации программ для ЭВМ.',
		'https://bhv.ru/wp-content/uploads/2021/02/Umrihin-Evgenij.jpg',
		'umrihin-evgenij'
	where not exists (select from Author where FirstName = 'Евгений' and LastName = 'Умрихин');

insert into Author (FirstName, MiddleName, LastName, Description, AuthorImage, UniqueId) 
	select 'Денис', 'Николаевич', 'Колисниченко', 'Колисниченко Денис Николаевич, инженер-программист и системный администратор. Имеет богатый опыт эксплуатации и создания локальных сетей от домашних до уровня предприятия, разработки приложений для различных платформ. Автор более 50 книг компьютерной тематики, в том числе “Microsoft Windows 10. Первое знакомство”, “Самоучитель Microsoft Windows 8”, “Программирование для Android 5. Самоучитель”, “PHP и MySQL. Разработка веб-приложений”, “Планшет и смартфон на базе Android для ваших родителей”, “”Linux. От новичка к профессионалу” и др.',
		'https://bhv.ru/wp-content/uploads/2020/08/Denis_Nikolavich_Kolisnichenko-e1646976464275-200x200.jpg',
		'kolisnichenko-denis-nikolaevich'
	where not exists (select from Author where FirstName = 'Денис' and LastName = 'Колисниченко');

---------------
-- ProductSerie
---------------
insert into ProductSerie (SerieName) 
	select 'Глазами хакера'
	where not exists (select from ProductSerie where SerieName = 'Глазами хакера');
	
insert into ProductSerie (SerieName) 
	select 'Внесерийные книги'
	where not exists (select from ProductSerie where SerieName = 'Внесерийные книги');
	
insert into ProductSerie (SerieName) 
	select 'В подлиннике'
	where not exists (select from ProductSerie where SerieName = 'В подлиннике');
	
insert into ProductSerie (SerieName) 
	select 'Дерзай! Набор электронных компонентов'
	where not exists (select from ProductSerie where SerieName = 'Дерзай! Набор электронных компонентов');

insert into ProductSerie (SerieName) 
	select 'С нуля'
	where not exists (select from ProductSerie where SerieName = 'С нуля');

insert into ProductSerie (SerieName) 
	select 'Самоучитель'
	where not exists (select from ProductSerie where SerieName = 'Самоучитель');

---------------
-- Category
---------------
insert into Category (CategoryName, CategoryUniqueId)
	select 'Компьютеры и программы', 'kompyutery-i-programmy'
	where not exists (select from Category where CategoryName = 'Компьютеры и программы');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Компьютеры и программы'), 
		'Программирование, Языки, Библиотеки', 'programmirovanie_yAzyki_biblioteki'

	where not exists (select from Category where CategoryName = 'Программирование, Языки, Библиотеки');
	
	


insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Компьютеры и программы'), 
		'Сети, Администрирование, Безопасность', 'seti_bezopasnost'
	where not exists (select from Category where CategoryName = 'Сети, Администрирование, Безопасность');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Компьютеры и программы'), 
		'Операционные системы', 'os'
	where not exists (select from Category where CategoryName = 'Операционные системы');


insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Компьютеры и программы'), 
		'Разработка Веб приложений', 'razrabotka_veb'
	where not exists (select from Category where CategoryName = 'Разработка Веб приложений');



insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Операционные системы'), 
		'Linux', 'linux'
	where not exists (select from Category where CategoryName = 'Linux');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Операционные системы'), 
		'Windows', 'windows'
	where not exists (select from Category where CategoryName = 'Windows');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Программирование, Языки, Библиотеки'), 
		'C/C++/C#', 'csharp'
	where not exists (select from Category where CategoryName = 'C/C++/C#');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Программирование, Языки, Библиотеки'), 
		'Java', 'java'
	where not exists (select from Category where CategoryName = 'Java');

insert into Category (ParentCategoryId, CategoryName, CategoryUniqueId)
	select 
		(select CategoryId from Category where CategoryName = 'Разработка Веб приложений'), 
		'Веб Сервер', 'veb_server'
	where not exists (select from Category where CategoryName = 'Веб Сервер');


insert into Category (CategoryName, CategoryUniqueId)
	select 'Наборы для мейкеров', 'nabory-dlya-mejkerov'
	where not exists (select from Category where CategoryName = 'Наборы для мейкеров');
	
insert into Category (CategoryName, CategoryUniqueId)
	select 'Технические науки', 'tehnicheskie-nauki'
	where not exists (select from Category where CategoryName = 'Технические науки');

insert into Category (CategoryName, CategoryUniqueId)
	select 'Учебная литература', 'uchebnaya-literatura'
	where not exists (select from Category where CategoryName = 'Учебная литература');

insert into Category (CategoryName, CategoryUniqueId)
	select 'Для детей', 'dlya-detej'
	where not exists (select from Category where CategoryName = 'Для детей');

insert into Category (CategoryName, CategoryUniqueId)
	select 'Досуг', 'dosug'
	where not exists (select from Category where CategoryName = 'Досуг');

---------------
-- Product
---------------
insert into Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'C/C++/C#'),
		'C# глазами хакера',
		560,
		'Подробно рассмотрены  все аспекты безопасности от теории до реальных реализаций .NET-приложений на языке C#. Рассказано, как обеспечивать безопасную регистрацию, авторизацию и поддержку сессий пользователей. Перечислены уязвимости, которые могут быть присущи веб-сайтам и Web API, описано, как хакеры могут эксплуатировать уязвимости  и как можно обеспечить безопасность приложений. Даны основы оптимизации кода для обработки максимального количества пользователей с целью экономии ресурсов серверов и денег на хостинг. Рассмотрены сетевые функции: проверка соединения, отслеживание запроса, доступ к микросервисам, работа с сокетами и др. Приведены реальные примеры атак хакеров и способы защиты от них.',
		'https://bhv.ru/wp-content/uploads/2023/04/2974_978-5-9775-1781-2.jpg',
		'2023-05-01',
		'c-glazami-hakera',
		(select ProductSerieId from ProductSerie where SerieName = 'Глазами хакера')
	where not exists (select from Product where UniqueId = 'c-glazami-hakera');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'c-glazami-hakera'), 'Articul', '2974'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'c-glazami-hakera') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select	(select ProductId from Product where UniqueId = 'c-glazami-hakera'),'ISBN', '978-5-9775-1781-2'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'c-glazami-hakera') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'c-glazami-hakera'), 'Pages', '352'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'c-glazami-hakera') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'c-glazami-hakera'), 'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'c-glazami-hakera') and ParamName = 'Print');


insert into Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'C/C++/C#'),
		'Библия C#. 5-е изд.',
		866,
		'Книга посвящена программированию на языке C#  для платформы Microsoft .NET, начиная с основ языка и разработки программ для работы в режиме командной строки и заканчивая созданием современных веб-приложений. Материал сопровождается большим количеством практических примеров. Подробно описывается логика выполнения каждого участка программы. Уделено внимание вопросам повторного использования кода. В пятом издании примеры переписаны с учетом современной платформы .NET 5, а вместо прикладных приложений упор делается на веб–приложения. На сайте издательства находятся коды программ, дополнительная справочная информация и копия базы данных для выполнения примеров из книги.',
		'https://bhv.ru/wp-content/uploads/2021/09/2853_978-5-9775-6827-2.jpg',
		'2023-01-01',
		'bibliya-c-5-izd',
		(select ProductSerieId from ProductSerie where SerieName = 'Внесерийные книги')
	where not exists (select from Product where UniqueId = 'bibliya-c-5-izd');


insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'), 'Articul', '2853'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'bibliya-c-5-izd') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'), 'ISBN', '978-5-9775-6827-2'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'bibliya-c-5-izd') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'), 'Pages', '464'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'bibliya-c-5-izd') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'), 'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'bibliya-c-5-izd') and ParamName = 'Print');



insert into Product (CategoryId, ProductName, Price,
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'C/C++/C#'),
		'Разработка Android-приложений на С# с использованием Xamarin с нуля',
		894,
		'Рассмотрены особенности создания Android-приложений при помощи Visual Studio Community на C#. Книга знакомит читателя со структурой проектов Xamarin.Android, с особенностями сборки и отладки приложений. Рассматриваются основные подходы к разработке Android-приложений, методы построения интерфейса, хранения данных, показана интеграция мобильных приложений с веб-службами, описаны особенности распространения и публикации приложений в магазине Google Play, разобраны основные методы монетизации мобильного контента. Представлены многочисленные примеры кода для решения различных задач, который можно использовать в своих приложениях.',
		'https://bhv.ru/wp-content/uploads/2021/02/2775_978-5-9775-6671-1.png',
		'2022-10-01',
		'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya',
		(select ProductSerieId from ProductSerie where SerieName = 'Внесерийные книги')
	where not exists (select from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya');


insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'Articul', '2775'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'ISBN', '978-5-9775-6671-1'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'), 'Pages', '304'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select 	(select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'),'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya') and ParamName = 'Print');


insert into Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'Linux'),
		'Linux глазами хакера. 6-е издание',
		894,
		'Рассмотрены вопросы настройки ОС Linux на максимальную производительность и безопасность. Описано базовое администрирование и управление доступом, настройка Firewall, файлообменный сервер, WEB-, FTP- и Proxy-сервера, программы для доставки электронной почты, службы DNS, а также политика мониторинга системы и архивирование данных. Приведены потенциальные уязвимости, даны рекомендации по предотвращению возможных атак и показано, как действовать при атаке или взломе системы, чтобы максимально быстро восстановить ее работоспособность и предотвратить потерю данных. В шестом издании обновлена информация с учетом последней версии Ubuntu и добавлено описание программ для тестирования безопасности конфигурации ОС. На сайте издательства размещены дополнительная документация и программы в исходных кодах.',
		'https://bhv.ru/wp-content/uploads/2021/03/2790_978-5-9775-6699-5-1.png',
		'2023-01-02',
		'linux-glazami-hakera-6-e-izdanie',
		(select ProductSerieId from ProductSerie where SerieName = 'Глазами хакера')
	where not exists (select from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Articul', '2790'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'ISBN', '978-5-9775-6699-5'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Pages', '416'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'), 'Print', '0' 
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie') and ParamName = 'Print');



insert into Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'Веб Сервер'),
		'Web-сервер глазами хакера. 3-е изд.',
		688,
		'Рассмотрена система безопасности web-серверов и типичные ошибки, совершаемые web-разработчиками при написании сценариев на языках PHP, ASP и Perl. Приведены примеры взлома реальных web-сайтов, имеющих уязвимости, в том числе и популярных. В теории и на практике рассмотрены распространенные хакерские атаки: DoS, Include, SQL-инъекции, межсайтовый скриптинг, обход аутентификации и др. Описаны основные приемы защиты от атак и рекомендации по написанию безопасного программного кода, настройка и способы обхода каптчи. В третьем издании рассмотрены новые примеры реальных ошибок, приведены описания наиболее актуальных хакерских атак и методов защиты от них.',
		'https://bhv.ru/wp-content/uploads/2021/06/2833_978-5-9775-6795-4.jpg',
		'2022-11-12',
		'web-server-glazami-hakera-3-e-izd',
		(select ProductSerieId from ProductSerie where SerieName = 'Глазами хакера')
	where not exists (select from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Articul', '2833'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'ISBN', '978-5-9775-6795-4'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Pages', '257'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'), 'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd') and ParamName = 'Print');



insert into Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'Linux'),
		'Linux. От новичка к профессионалу. 8 изд.',
		1210,
		'Даны ответы на все вопросы, возникающие при работе с Linux: от установки и настройки этой ОС до настройки сервера на базе Linux. Материал книги максимально охватывает все сферы применения Linux от запуска Windows-игр под управлением Linux до настройки собственного веб-сервера. Также рассмотрены: вход в систему, работа с файловой системой, использование графического интерфейса, установка программного обеспечения, настройка сети и Интернета, работа в Интернете, средства безопасности, резервное копирование, защита от вирусов и другие вопросы. Материал ориентирован на последние версии дистрибутивов Fedora, openSUSE, Slackware, Ubuntu.',
		'https://bhv.ru/wp-content/uploads/2021/11/2822_978-5-9775-6773-2.jpg',
		'2019-09-12',
		'linux-ot-novichka-k-professionalu-8-izd',
		(select ProductSerieId from ProductSerie where SerieName = 'В подлиннике')
	where not exists (select from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Articul', '2822'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'),'ISBN', '978-5-9775-6773-2'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Pages', '688'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'), 'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd') and ParamName = 'Print');


insert into Product (CategoryId, ProductName, Price, 
				  	Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId)
	select (select CategoryId from Category where CategoryName = 'Windows'),
		'Самоучитель Microsoft Windows 11',
		787,
		'Описаны как базовые функции, так и основные новинки Windows 11: улучшенный интерфейс системы и новое стартовое меню, полностью переработанный браузер Microsoft Edge, вход на основе биометрии Windows Hello, русскоязычный голосовой ввод, функция работы с многими окнами Snap Layouts.  Рассмотрены среда восстановления и резервное копирование системы, сетевой диск OneDrive, магазин Microsoft Store и другие возможности Windows 11. Особое внимание уделено практическому использованию операционной системы – рассказано, как использовать обновленный файловый менеджер Проводник, как подключиться к Интернету и как решить возможные проблемы с сетевым подключением, как выполнить S.M.A.R.T.-диагностику накопителя и перенести систему на SSD.  Дополнительно описана программа Skype. Книга богато иллюстрирована, что поможет освоить новую систему наглядно и быстро.',
		'https://bhv.ru/wp-content/uploads/2021/11/2880_978-5-9775-6872-2.jpg',
		'2020-04-12',
		'samouchitel-microsoft-windows-11',
		(select ProductSerieId from ProductSerie where SerieName = 'Самоучитель')
	where not exists (select from Product where UniqueId = 'samouchitel-microsoft-windows-11');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'), 'Articul', '2880'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11') and ParamName = 'Articul');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'),'ISBN', '978-5-9775-6829-6'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11') and ParamName = 'ISBN');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'), 'Pages', '368'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11') and ParamName = 'Pages');

insert into ProductDetail (ProductId, ParamName, StringValue )
select (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'), 'Print', '0'
where not exists (select from ProductDetail where ProductId = (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11') and ParamName = 'Print');


---------------
-- ProductAuthor
---------------
insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'c-glazami-hakera'),
		(select AuthorId from Author where FirstName = 'Михаил' and LastName = 'Фленов')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'c-glazami-hakera'));
	
insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'),
		(select AuthorId from Author where FirstName = 'Михаил' and LastName = 'Фленов')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'bibliya-c-5-izd'));

insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'),
		(select AuthorId from Author where FirstName = 'Евгений' and LastName = 'Умрихин')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'razrabotka-android-prilozhenij-na-s-s-ispolzovaniem-xamarin-s-nulya'));

insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'),
		(select AuthorId from Author where FirstName = 'Михаил' and LastName = 'Фленов')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'linux-glazami-hakera-6-e-izdanie'));

insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'),
		(select AuthorId from Author where FirstName = 'Михаил' and LastName = 'Фленов')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'web-server-glazami-hakera-3-e-izd'));

insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'),
		(select AuthorId from Author where FirstName = 'Денис' and LastName = 'Колисниченко')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'linux-ot-novichka-k-professionalu-8-izd'));


insert into ProductAuthor (ProductId, AuthorId)
	select (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'),
		(select AuthorId from Author where FirstName = 'Денис' and LastName = 'Колисниченко')
	where not exists (select from ProductAuthor where ProductId = (select ProductId from Product where UniqueId = 'samouchitel-microsoft-windows-11'));

