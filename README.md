#  Altura NFT Unity SDK 

For game developers to integrate Smart NFTs into thier Unity game using out C# Library.


# Altura SDK installation

Start by importing this project as a Unity package.

After successfully installing the Altura Unity SDK, developers can use the AlturaSDK to mack calls to the Altura API endpoints
Once imported, you should be able to make async calls against the Web3Manager static class for 

- AuthenticateUser(address, code)
- AuthenticateWallet(code)
- GetUser(address)
- GetItem(collecttionAddress, tokenId)
- GetCollection(address)
- GetUsers(perPage, page, sortBy, sortDir)
- GetItems(perPage, page, sortBy, sortDir, slim)
- GetCollections(perPage, page, sortBy, sortDir))
