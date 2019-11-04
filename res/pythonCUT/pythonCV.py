
import sys
import cv2 as cv2
import numpy as np 


# read command-line arguments
filename = sys.argv[1]

# read original image
image = cv2.imread(filename = filename)



# create binary image


gray = cv2.cvtColor(cv2.UMat(image), cv2.COLOR_BGR2GRAY)

gray = cv2.bilateralFilter(gray, 10, 17, 17) 

sigma=10
v = np.median(image)
lower = int(max(5, (1.0 - sigma) * v))
upper = int(min(170, (1.0 + sigma) * v))
edged = cv2.Canny(image, lower, upper)

cv2.imshow("Display window",edged)
cv2.waitKey(0)

t, binary = cv2.threshold(src = edged, thresh = 70, maxval =100, type = cv2.THRESH_BINARY)
contours, hierarchy = cv2.findContours(image = binary, mode = cv2.RETR_TREE,method = cv2.CHAIN_APPROX_SIMPLE)
cv2.drawContours(image,contours,-1,(0,0,255))
cv2.imshow("Display window",image)
cv2.waitKey(0)

print("Found %d objects." % len(contours))
# for (i, c) in enumerate(contours):
#     print("\tSize of contour %d: %d" % (i, len(c)))
#     roi_color = contours
    
    