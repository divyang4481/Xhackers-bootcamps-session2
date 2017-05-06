function CreateGuid() {  
   function _p8(s) {  
      var p = (Math.random().toString(16)+"000000000").substr(2,8);  
      return s ? "-" + p.substr(0,4) + "-" + p.substr(4,4) : p ;  
   }  
   return _p8() + _p8(true) + _p8(true) + _p8();  
}  
  

var api = {

    // To create a Shared Access Signature (SAS), use the generateSharedAccessSignature method
    get: (request, response, next) => {

            var containerName = 'photos';
            var guid = CreateGuid();
            var blobName = guid + '.png';
            var accountName = 'gpstagimagestorage';
            var accountKey = 'y8zzh5OH5l3mAQoP/MVL/3ZSsJT/YomNvO/4zur2LSACCEtyMLCbAw3WEH65ctqjrkk23/jgPh0/5fGSPizbbA==';
            var azure = require('azure-storage');
            var blobService = azure.createBlobService(accountName,accountKey);
            var startDate = new Date();
            var expiryDate = new Date(startDate);
            expiryDate.setMinutes(startDate.getMinutes() + 100);
            startDate.setMinutes(startDate.getMinutes() - 100);
            var sharedAccessPolicy = {
            AccessPolicy: {
            Permissions: 'rwc',
            Start: startDate,
            Expiry: expiryDate
            },
      };

        var token = blobService.generateSharedAccessSignature(containerName, blobName, sharedAccessPolicy);
        var sasUrl = blobService.getUrl(containerName, blobName, token);
        response.send(JSON.stringify(sasUrl));
    }
};


module.exports = api;