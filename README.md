# Online Guitar Rentals
A mockup guitar rental system website built using .NET Core 3.0 with a SQL Server database for storage!

## Main Page
![MainScreen](https://user-images.githubusercontent.com/52103944/113383503-12fb6a00-9352-11eb-80bc-d2b3cb938da1.png)

### Main Page Walkthrough
![MainScreen](https://user-images.githubusercontent.com/52103944/113384011-222ee780-9353-11eb-9638-777dda4a7ec2.gif)

#### Notable Features:
* Cell Block highlighting
* Selecting a cell block will bring you to a filtered version of the Products Page displaying only those types of guitars
* Clicking the OGR logo in the top left always brings you back to the Home Page

## Products Page
![ProductsScreen](https://user-images.githubusercontent.com/52103944/113383920-ebf16800-9352-11eb-9af9-30fa43c76352.png)

### Product Detail Page
![ProductDetailScreen](https://user-images.githubusercontent.com/52103944/113383917-eac03b00-9352-11eb-94da-26acbeb1790e.png)

### Product Page Walkthrough
![ProductsScreen](https://user-images.githubusercontent.com/52103944/113383919-eb58d180-9352-11eb-84b3-0d283779dd4a.gif)

![ProductDetailScreen](https://user-images.githubusercontent.com/52103944/113383915-ea27a480-9352-11eb-9f7d-65bcb402d72b.gif)

#### Notable Features:
* Search filter
* Ability to upload multiple photos with different perspectives for each product 
---

## Subscribers Page
![SubscriberIndexScreen](https://user-images.githubusercontent.com/52103944/113383923-ebf16800-9352-11eb-8291-0cb36e79ad0d.png)

### Subscriber Detail Page
![SubscriberDetail](https://user-images.githubusercontent.com/52103944/113383921-ebf16800-9352-11eb-9894-ff44a28a5854.png)


### Subscriber Page Walkthrough
![SubscriberScreen](https://user-images.githubusercontent.com/52103944/113383881-daa85b80-9352-11eb-9382-5f50ccadb61a.gif)

#### Notable Features:
* Direct links to each subscriber's profile
* Sorting on the Last Name, First Name, Renewal Date, Expiration Date, and Active columns
---

## Distribution Centers Page
![DistrubtuionIndexScreen](https://user-images.githubusercontent.com/52103944/113383910-e98f0e00-9352-11eb-8632-2e4d560d056f.png)

### Distribution Center Detail Page
![DistrubtuionDetailScreen](https://user-images.githubusercontent.com/52103944/113383909-e98f0e00-9352-11eb-89a1-fa5d8e8e4f36.png)

### Distribution Center Page Walkthrough
![DistributionScreen](https://user-images.githubusercontent.com/52103944/113383906-e8f67780-9352-11eb-9b6c-1b954d89b499.gif)

#### Notable Features:
* Direct link to each Distribution Center Page
* Sorting on the Distribution Center, Delivering Now, Number of Assets, Total Asset Value, Number of Subscribers Serviced columns
* Dynamically updated column that tracks the shipping hours for each distribution center by checking if the user's system time is within the service hours for any of the couriers
* Aggreagted Subscribers counts and Total Asset values
---

## Inventory Ledger Page
![InventoryLedger](https://user-images.githubusercontent.com/52103944/113383911-e98f0e00-9352-11eb-80c6-b81f6b4402b0.png)

### Inventory Ledger Page Walkthrough
![InventoryScreen](https://user-images.githubusercontent.com/52103944/113388067-881f6d00-935b-11eb-8dc3-a8905870d563.gif)

#### Notable Features
* Direct link to each asset's product detail page
* Sorting on the Product ID, Asset Brand, Asset Name, Asset Type, Asset Style, Total Stock, Total Asset Value columns
* Aggregated Stock and Asset values
---

##### Future Improvements
###### FrontEnd:
* Completely re-do the design; it was initially used as a canvas to become more comfortable with HTML/CSS/Javascript
*  Write media queries to make mobile compatible
###### Backend:
* Optimize Entity Framework Core
  * Currently, ZERO Optimizations on the database layer
  * Would be completely unusable in an actual production environment with current EFC configuration
* Create User Services/Controller
* Buildout to service guitar peripherals (Amps, Straps, Picks, etc.)
* Add functionality so that renewal/expiration dates remain consistent
* Allow users to login and have their subscriber IDs be applied natively throughout their session
###### Other Minor Changes:
*  Textbox to keep tabular consistency
*  Improve search bar 
*  Handle grid views for odd number of photos
*  Change the table headers to show which being sorted in what order
*  Pagination
