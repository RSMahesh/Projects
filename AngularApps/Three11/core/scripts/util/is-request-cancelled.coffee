# Returns'true' if a request was cancelled (via a timeout promoise)
#
# Author: Markus Westerholz
# Date: 2015/03/13
#
# Copyright © 2015 MediaValet, Inc.
# All rights reserved
'use strict'

define -> (req)-> !!(req.config or req)?.timeout?.$$state?.status
