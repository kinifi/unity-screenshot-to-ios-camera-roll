//
//  SaveToAlbum.m
//  cameratest
//
//  Created by Christopher Figueroa on 5/25/18.
//  Copyright © 2018 kinifi. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface SaveToAlbum : NSObject
    typedef void (*DelegateCallbackOnCompleteFunction)();
@end

@implementation SaveToAlbum


//delegate for sending data to unity
DelegateCallbackOnCompleteFunction __delegate = nil;

//the method that sets the callback function for oncomplete
- (void) setOnCompleteDelegate:(DelegateCallbackOnCompleteFunction)onComplete {
    __delegate = onComplete;
}

//Plist needs to have this
//<key>NSPhotoLibraryUsageDescription</key>
//<string>This app needs access to photos.</string>
- (void)saveImageToCamera:(char *)imagebytes dataSize:(int) sizeOfImageData {
    
    NSData* imageData = [NSData dataWithBytes:imagebytes length:sizeOfImageData];
    UIImage* image = [[UIImage alloc] initWithData:imageData];
    UIImageWriteToSavedPhotosAlbum(image, nil, nil, nil);
    __delegate();
    NSLog(@"Saved Image To Camera Roll");
}


@end



#pragma mark -
#pragma mark C Methods

#ifdef __cplusplus
extern "C" {
#endif
    
    static SaveToAlbum *cameraRollController = nil;
    
    void initialize() {
        if(cameraRollController == nil) {
            cameraRollController = [[SaveToAlbum alloc] init];
            NSLog(@"Init has completed");
        }
        
    }
    
    void saveImageToAlbum(char* imagebytes, int sizeOfImageData) {

        [cameraRollController saveImageToCamera:imagebytes dataSize:sizeOfImageData];
        
    }

    void setOnCompleteCallback(DelegateCallbackOnCompleteFunction callback) {

        [cameraRollController setOnCompleteDelegate:callback];
    }

    void deInitialize() {
        cameraRollController = nil;
    }
    
#ifdef __cplusplus
}
#endif
