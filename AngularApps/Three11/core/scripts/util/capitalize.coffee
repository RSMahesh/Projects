# This utility capitalizes the passed in string 's'.

define ->
  return (s) -> return if s then s[0].toUpperCase() + s[1..] else ''
