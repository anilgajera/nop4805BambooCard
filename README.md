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

![Install](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007514-241dce1fae35d144b36e883fcb6efd75.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T021004Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=415f76f75b5ff932aefb6aeecd85f81351a7355db1a58ed24677be9036cb338f)


### 1. Develop a Simple NopCommerce Plugin [DiscountRules.CustomDiscounts] ###

* Plugin will auto install on store setup, Below are the steps for separate plugin installation
* Save DiscountRules.CustomDiscounts Plugin in "\Presentation\Nop.Web\Plugins\\" path
* Go to Admin area -> Configuration -> Local Plugins and find "Custom Discounts" plugin
* Click on Install button
![Menu](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007545-3e14d30351d3b25a38d5e40f8e14e3ce.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T021847Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=dbbf1e571d1b2802364f17c1588950b2864e38d19dbd1373fe9df72d7a47014c)
* Create a Disount in Admin area -> Promotions -> Discounts
![Discount](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007574-2930b5170082f98263d60e86070ef598.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T022357Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=c6fd348d8808d46d395d7bcf69e141ed1c2492994e9363ecab91bd0047d55f5b)
* When you are going to place 4th order and more, you will get a discount of 10% on the order total
 ![Discount](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007606-7f75f865be00a3b3e6d59652e474f784.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T023140Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=08f1b0d13cf9b5f07275089fa0ed3fc23b7f86bfe749ccd240600aa845144d2d)

### 2. Modify the Checkout Process ###
* Add a “Gift Message” field at checkout.
![CartPage](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007584-fc88386f6934a08cd466984e00e84201.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T022540Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=5144c30e5bc9af5826c6bc5bc60489210c33288979d2c186f3fd5b15a8857363)
![AdminPage](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007598-26f74ec6b41dd143615cea008f1ec565.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T022807Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=653f8000e1a7388266e761a62d63ca896b79cd72098f4d14ebf229e5c0c8044c)

### 3. Allow to search by "Name" on the product attribute page (admin area) ###
* Go to Admin area -> Catalog -> Attributes -> Product attributes
![Name](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007655-503eb03fa5d283e6db390b0438864009.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T024714Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=bee8d2ccaa3a3da99429b1308dd5318e07d231dfe84512f391362e844b03b19b)

### 4. Test the API endpoint. ###

* I have installed swagger in application, you can test using swagger UI
* Open the swagger UI using the URL: http://localhost/api/index.html
![Swagger](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007675-098989836d97e89f1fd9d86d0966dbca.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T025127Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=28993d3478cce9bb56f5b4b5a707bb44ba869247b3b667daf8ef54bfefbcad91)

 
* Get JWT token from POST http://localhost/api/Authenticate/GetToken API using login email and password
![Token Request](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007692-c3a9a3af1c3f34f1d42ce44f2cda9cab.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T025551Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=ec86be66048a14cc43522d89afe181393bb3eaf19a062764b3630f23d1674b2c)
![Token Response](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007699-a0d72d21e696dbbed9864dc0e71bf840.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T025734Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=2dc1803d3ac109d0fa02506ff9e8ae8ded6efa6d44afcf4edc045c7cbd64ffe6)
* Click on "Authorize" button and enter the token in the format "Bearer {{token}}".
![Authorize](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007707-b169b0bdcd4a2f38d03ad52482a8a2fd.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T025939Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=4da97eac725682e2da2dac7306b7c3177ad8a107e753f884da7a417aaf6ed1a2)
* Get Order Data from GET http://localhost/api/Order/OrderDetails/{{email}} API using customer email address
![Order Request](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007717-f493ebfe015966306bf4606807c13490.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T030121Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=e1fa486853560e6fc8da3c83856bae14cefc6e28638560a4224b98f13032dec7)
![Order Response](https://awesomescreenshot.s3.amazonaws.com/image/6331037/54007722-3c1b1444664befc743080991df13e327.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJSCJQ2NM3XLFPVKA%2F20250419%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250419T030232Z&X-Amz-Expires=28800&X-Amz-SignedHeaders=host&X-Amz-Signature=b026d4fa1e05eb30615d960ab6ea29dd0adc7b49246bb5b912f7abbb4302e534) 

### 5. Containerization & Quick Deployment Setup ###
* Created a docker file to build the image and run the container

