### Setup a nopCommerce on Docker ###

* Pre requesites
   * Docker installed on your machine
   * Disable application which are using port 80 (IIS) and 1433(MS SQL Server)
   
[Create docker container](https://docs.nopcommerce.com/en/developer/tutorials/docker.html)
* Build the Docker container. For the convenience of executing commands, go to the directory where Dockerfile is located (the root directory of the nopCommerce source files).
* The command that we need: 
*     docker build -t nopcommerce .
* This command builds the container according to the instructions described in the "Dockerfile" file. The first launch of the assembly will take a lot of time since it will require downloading two basic images for .Net Core applications.
* SQL server that our container can access. But, as a rule, our and user environments are limited, so we have prepared a layout file that will allow you to deploy the nopCommerce container in conjunction with the container containing the SQL server.
* To deploy container composition, use the command: 
*     docker-compose up -d
* This command uses the docker-compose.yml file for deployment, which describes the creation of two containers "nopcommerce_web" and "nopcommerce_database", which provide a bundle of applications and a database.

* And by opening the page on the browser we will be able to test everything we want. To connect to the database server, we use the following data (as described in the docker-compose.yml file):
* You can coonect using SSMS with Server Name: IP *127.0.0.1* User: *sa* Password: *nopCommerce_db_password*
*     Server name: nopcommerce_mssql_server User: sa Password: nopCommerce_db_password

![Install](https://raw.githubusercontent.com/anilgajera/nop4805BambooCard/refs/heads/main/Images/nopCommerce%20installation.png)


### 1. Develop a Simple NopCommerce Plugin [DiscountRules.CustomDiscounts] ###

* Plugin will auto install on store setup, Below are the steps for separate plugin installation
* Save DiscountRules.CustomDiscounts Plugin in "\Presentation\Nop.Web\Plugins\\" path
* Go to Admin area -> Configuration -> Local Plugins and find "Custom Discounts" plugin
* Click on Install button
![Menu](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Local%20plugins%20_%20nopCommerce%20administration.png?raw=true)
* Create a Disount in Admin area -> Promotions -> Discounts
![Discount](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Edit%20discount%20details%20_%20nopCommerce%20administration.png?raw=true)
* When you are going to place 4th order and more, you will get a discount of 10% on the order total
 ![Discount](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Your%20store.%20Shopping%20Cart%20(1).png?raw=true)

### 2. Modify the Checkout Process ###
* Add a “Gift Message” field at checkout.
![CartPage](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Your%20store.%20Shopping%20Cart.png?raw=true)
![AdminPage](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Edit%20order%20details%20_%20nopCommerce%20administration.png?raw=true)

### 3. Allow to search by "Name" on the product attribute page (admin area) ###
* Go to Admin area -> Catalog -> Attributes -> Product attributes
![Name](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Product%20attributes%20_%20nopCommerce%20administration.png?raw=true)

### 4. Test the API endpoint. ###

* I have installed swagger in application, you can test using swagger UI
* Open the swagger UI using the URL: http://localhost/api/index.html
![Swagger](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI.png?raw=true)

 
* Get JWT token from POST http://localhost/api/Authenticate/GetToken API using login email and password
![Token Request](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI%20(1).png?raw=true)
![Token Response](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI%20(2).png?raw=true)
* Click on "Authorize" button and enter the token in the format "Bearer {{token}}".
![Authorize](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI%20(3).png?raw=true)
* Get Order Data from GET http://localhost/api/Order/OrderDetails/{{email}} API using customer email address
![Order Request](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI%20(4).png?raw=true)
![Order Response](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/Swagger%20UI%20(5).png?raw=true) 

### 5. Containerization & Quick Deployment Setup ###
* Created a docker file to build the image and run the container
 ![Docker Image](https://github.com/anilgajera/nop4805BambooCard/blob/main/Images/nopCommerce%20docker.jpg?raw=true)

