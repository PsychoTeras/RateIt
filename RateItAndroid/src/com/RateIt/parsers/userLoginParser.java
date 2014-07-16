package com.RateIt.parsers;

import android.util.Base64;
import com.RateIt.entity.UserLoginQueryResult;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;

/**
 * Created by igor on 7/16/14.
 */
public class userLoginParser extends BaseJsonParser {

    private final String UserId = "UserId";
    private final String SessionId = "SessionId";

    public userLoginParser(String jsonData) throws JSONException {
        super(jsonData);
    }

    public userLoginParser(JSONObject jsonData) throws JSONException {
        super(jsonData);
    }

    public UserLoginQueryResult parse() throws UnsupportedEncodingException {
        UserLoginQueryResult result = new UserLoginQueryResult();
        if(hasNonEmpty(UserId)){
            result.setUserId(getString(UserId));
        }
        if(hasNonEmpty(SessionId)){
            result.setSessionId(getString(SessionId));
        }
        if(hasNonEmpty(ErrorCode)){
            result.setErrorCode(getInt(ErrorCode));
        }
        if(hasNonEmpty(ErrorMessage)){
            result.setErrorMessage(getString(ErrorMessage));
        }
        return result;
    }
}
