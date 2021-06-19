# macos-photo-movie-compressor
tool to quickly find large `mov` files in the photo library and compress them, preserving metadata.

I found i was running out of iCloud storage and didn't want to make the jump from 200Gb to 2Tb. I have lots of movies from my phone in my library and these were often filmed at 1080p60 which is excessive for what i need. It's just random clips of my kids.

Editing files in photos in macos only makes ghe library bigger as it keeps all originals, so even where i had cropped the files down the original files were still there - and often multi-gigabyte.

I considered making a shell script or automator action to do this, but it was easier for me t ojust write a few lines of code extract the movies, compress them, copy the meta data across then manuall add them back to the library, and then deleting the originals by hand.

I did the top 100 movies in the library and recovered about 50Gb of space, which will hopefully last me a few more years before im forced to pay apple more money.

## Prerequisites
* Handbrake CLI https://handbrake.fr/downloads2.php
* Exif Tool https://exiftool.org/
* Grant the dotnet process permission to access all files in the file system (else you get permission exceptions)

# Manual Steps
1. run the dotnet app to collect all the movies and compress them
1. add all the files back to photos
1. from the recently imported folder in photos create an album of all the imported videos
1. using the album, right click each video and `show in all photos`
1. as the meta data is preserved the new and old videos should be side by side
1. delete the old video
1. back to the album, delete the video from there
1. repeat until the album folder is empty

I did the top 100 videos and this manual process took a while, but i dont see how it could be automated.

# To be improved
The original file name is lost as all files inside the phot library are just guids. That means when the file is reimported you'll loose any additional comments or names you had applied. Though its not hard to copy it across as you manuall process each file.

