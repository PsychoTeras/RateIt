package com.RateIt.network;

import android.content.Context;
import android.util.Base64;
import com.RateIt.entity.UserLoginQueryResult;
import com.RateIt.parsers.userLoginParser;
import com.RateIt.utils.Logger;
import com.RateIt.network.ApiFunction;
import com.RateIt.network.ApiJSONFunction;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class LoginCall{

    private ApiJSONFunction func;
    UserLoginQueryResult userLoginResult;

	public LoginCall(Context context, String params) {
        func = new ApiJSONFunction(context, ApiFunction.USER_LOGIN + params);
	}

    public boolean call(){
        boolean result = func.call();
        JSONObject response = func.getResponse();
        if(response == null){
            return false;
        }
        try{
            userLoginResult = new userLoginParser(response).parse();
        }catch(Exception e){
            Logger.e("LoginCall", e);
        }
        return result;
    }

    public UserLoginQueryResult getUserLoginResult()
    {
        return userLoginResult;
    }

    // createParamRow(user, pass, tid);
    public static String createParamRow(String user, String pass, String tid) throws UnsupportedEncodingException {
        String params="?";
        params+="userName="+ URLEncoder.encode(user, "utf-8");
        params+="&?passwordHash="+getHash(pass);
        params+="&?tid="+tid;
        return params;
    }

    private static String getHash(String password) throws UnsupportedEncodingException {

        byte[] key = new byte[0];
        try {
            key = password.getBytes("UTF-8");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        MessageDigest md = null;
        try {
            md = MessageDigest.getInstance("SHA-1");
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        byte[] hash = md.digest(key);

        String result = Base64.encodeToString(hash, Base64.DEFAULT);

        return URLEncoder.encode(result.replace("\n", ""), "utf-8");
    }
}
