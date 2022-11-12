
 <div id="top"></div>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://alturanft.com">
    <img src="https://www.alturanft.com/logo-svg.svg" alt="Logo" width="80" height="80">
  </a>

  <h2 align="center">Altura Gaming Unity SDK </h2>

  <p align="center">
    <p>Powering the Future of Gaming</p>
    <br />
    <a href="https://alturanft.com/">Landing Page</a>
    ·
    <a href="https://app.alturanft.com/">Marketplace</a>
    ·
    <a href="https://github.com/alturanft/JS-SDK/issues">Report Bug</a>
    ·
    <a href="https://github.com/alturanft/JS-SDK/issues">Request Feature</a>
  </p>
</div>
<br />
<br />
<br />

### Introduction
NFTPort's Unity extension provides tools and features to fast-track development and create cross-chain compatible NFT games and products in Unity with fast and reliable onchain data.

# **Installation**
Using Altua NFT Unity SDK, you are able to interact with Altura services such as authenticating users using [Altura Guard](https://docs.alturanft.com/altura-developer-api/getting-started/altura-guard), get Users information and items, get items and collection information and transfer or mint new items.

# installation
1. Open Unity and then open `Window > Package Manager`
2. In Package Manager, click <b>+</b> and select <b>Add Package from git URL</b>
3. Paste 
  ```
  https://github.com/alturanft/Unity-SDK.git
  ```
4. Click <b>Add</b>.  

After you added Altura SDK to Unity, additional packages needs to be installed to do this:
1. Open open `Altura NFT > Install Dependencies`
2. Than click on <b>Install Now</b>

Altura SDK installed successfully!

# Import in in your script
  ```C#
  using AlturaNFT;
  ```

# Import Samples
To import some samples made by Altura Team do:
1. Open Unity and then open `Window > Package Manager` 
2. Select <b>Altura Web3<b>
3. Select <Samples> and import


# Methods
 
| Method     | Description    |
| AuthenticateUser(address, code)     | Use this method to authenticate user with Altura Guard. 

On success, `true` is returned.    |
| GetUser(address)     | Description    |
| GetUsers(perPage, page, sortBy, sortDir)     | Description    |
| GetItem(collecttionAddress, tokenId)     | Description    |
| GetItems(perPage, page, sortBy, sortDir, slim)     | Description    |
| GetCollection(address)     | Description    |
| GetCollections(perPage, page, sortBy, sortDir)     | Description    |
| TransferItem(collectionAddress, tokenId, amount, to)     | Description    |
| TransferItems()     | Description    |
| MintAdditionalSupply()     | Description    |
| GetUserItems()     | Use this method to get items of specific user.

On success, the data is returned as JSON.    |
| GetHolders()     | Description    |
| GetHistory()     | Description    |
| UpdateProperty()     | Description    |
| UpdatePrimaryImage()     | Description    |
| Update()     | Description    |

